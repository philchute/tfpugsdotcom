using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages
{
    public class AchievementsModel : PageModel
    {
        private readonly ILogger<AchievementsModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<ApiSystemAchievement> DisplayAchievements { get; private set; } = new List<ApiSystemAchievement>();
        public string? ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "game"; // game, name, percentage

        public List<string> GameDisplayOrder { get; private set; } = new List<string>();

        // We now need to group by game, and then by tag within that game.
        // Outer key: GameId (e.g., "TFC", "Global")
        // Inner key: Tag (e.g., "Combat", "Special")
        // Value: List of achievements for that game and tag.
        public Dictionary<string, Dictionary<string, List<ApiSystemAchievement>>> GroupedByGameAndTag { get; private set; } = new Dictionary<string, Dictionary<string, List<ApiSystemAchievement>>>();

        public Dictionary<string, List<ApiSystemAchievement>> GroupedAchievements { get; private set; } = new Dictionary<string, List<ApiSystemAchievement>>();

        public AchievementsModel(ILogger<AchievementsModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            var apiClient = _httpClientFactory.CreateClient();
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];

            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                _logger.LogError(ErrorMessage);
                return;
            }

            var achievementsApiUrl = $"{apiBaseUrl}/api/achievements";

            try
            {
                _logger.LogInformation("Fetching all achievements from: {AchievementsApiUrl}", achievementsApiUrl);
                var response = await apiClient.GetAsync(achievementsApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var allAchievements = JsonSerializer.Deserialize<List<ApiSystemAchievement>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (allAchievements != null)
                    {
                        SortAndGroupAchievements(allAchievements);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching achievements: {response.StatusCode}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }

        private void SortAndGroupAchievements(List<ApiSystemAchievement> achievements)
        {
            // First, sort the achievements based on the user's selection to ensure a consistent order within groups.
            IEnumerable<ApiSystemAchievement> sortedAchievements;

            switch (SortBy.ToLower())
            {
                case "name":
                    sortedAchievements = achievements.OrderBy(a => a.Name);
                    break;
                case "percentage":
                    sortedAchievements = achievements.OrderByDescending(a => a.HolderPercentage ?? -1.0);
                    break;
                case "game":
                default:
                    // When sorting by game, we also sort by name to have a predictable order in the final grouping.
                    sortedAchievements = achievements
                        .OrderBy(a => a.GameId ?? " ")
                        .ThenBy(a => a.Name);
                    break;
            }

            DisplayAchievements = sortedAchievements.ToList();

            // Grouping for the view, now by game and then by tag
            var groupedByGame = DisplayAchievements.GroupBy(a => a.GameId ?? "Server");

            foreach (var gameGroup in groupedByGame)
            {
                var gameName = gameGroup.Key;
                var achievementsInGame = gameGroup.ToList();
                var groupedByTag = new Dictionary<string, List<ApiSystemAchievement>>();

                foreach (var ach in achievementsInGame)
                {
                    if (ach.Tags != null && ach.Tags.Any())
                    {
                        foreach (var tag in ach.Tags)
                        {
                            if (!groupedByTag.ContainsKey(tag))
                            {
                                groupedByTag[tag] = new List<ApiSystemAchievement>();
                            }
                            groupedByTag[tag].Add(ach);
                        }
                    }
                    else
                    {
                        // Handle achievements with no tags
                        const string noTagKey = "General";
                        if (!groupedByTag.ContainsKey(noTagKey))
                        {
                            groupedByTag[noTagKey] = new List<ApiSystemAchievement>();
                        }
                        groupedByTag[noTagKey].Add(ach);
                    }
                }
                GroupedByGameAndTag[gameName] = groupedByTag;
            }


            // Establish the order for displaying game sections
            GameDisplayOrder.Add("Global");
            if (GroupedByGameAndTag.ContainsKey("TFC")) 
            {
                GameDisplayOrder.Add("TFC");
            }
            // Add remaining games, ordered alphabetically
            foreach (var gameKey in GroupedByGameAndTag.Keys.OrderBy(k => k))
            {
                if (gameKey != "Global" && gameKey != "TFC")
                {
                    GameDisplayOrder.Add(gameKey);
                }
            }
        }
    }
} 