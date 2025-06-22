using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages
{
    public class LeaderboardsModel : PageModel
    {
        private readonly ILogger<LeaderboardsModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<LeaderboardDisplay> ItemsToDisplay { get; private set; } = new();
        public string? ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; } = "active";

        [BindProperty(SupportsGet = true)]
        public string? NameLike { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedGameId { get; set; } = "TFC";

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 100;

        [BindProperty(SupportsGet = true)]
        public string Metric { get; set; } = "elo";

        public List<Game> AllGames { get; private set; } = new();
        public List<SelectListItem> GameOptions { get; set; } = new List<SelectListItem>();
        public string SelectedParticipantType { get; private set; } = "player";
        
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }

        public List<SelectListItem> PageSizeOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "50", Text = "50" },
            new SelectListItem { Value = "100", Text = "100" },
            new SelectListItem { Value = "200", Text = "200" },
        };

        public List<SelectListItem> MetricOptions { get; private set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? GoToPage { get; set; }

        public LeaderboardsModel(ILogger<LeaderboardsModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            await FetchGamesAsync();
            
            try
            {
                var selectedGame = AllGames.FirstOrDefault(g => g.GameId == SelectedGameId);
                SelectedParticipantType = selectedGame?.ParticipantType ?? "player";

                // If the participant type is team, ensure the metric is valid for teams.
                if (SelectedParticipantType == "team" && (Metric == "gems" || Metric == "xp"))
                {
                    Metric = "elo"; // Default to a valid metric for teams
                }

                var allItems = await FetchAllLeaderboardData(SelectedGameId, SelectedParticipantType);
                ProcessAndFilterItems(allItems);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }

        private async Task<List<LeaderboardDisplay>> FetchAllLeaderboardData(string gameId, string participantType)
        {
            var allItems = new List<LeaderboardDisplay>();
            var apiClient = _httpClientFactory.CreateClient("TfcEloApiClient");
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new InvalidOperationException("API Base URL is not configured.");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
            int currentPage = 1;
            bool hasMorePages = true;

            while(hasMorePages)
            {
                string requestUrl;
                if (participantType == "team")
                {
                    requestUrl = $"{apiBaseUrl}/api/teams?game_id={gameId}&page={currentPage}&page_size=500";
                }
                else
                {
                    requestUrl = $"{apiBaseUrl}/api/leaderboard?game_id={gameId}&page={currentPage}&page_size=500";
                }
                
                var response = await apiClient.GetAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError("API Error fetching leaderboard data from {Url}. Status: {StatusCode}. Body: {Body}", requestUrl, response.StatusCode, errorBody);
                    throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();

                if (participantType == "team")
                {
                    var apiData = JsonSerializer.Deserialize<TeamsListApiResponse>(json, options);
                    if (apiData != null && apiData.Teams.Any())
                    {
                        allItems.AddRange(apiData.Teams.Select(t => new LeaderboardDisplay { Id = t.TeamId, Name = t.Name, ImageUrl = t.ImageUrl, Elo = t.Elo, Wins = t.Wins ?? 0, Losses = t.Losses ?? 0, Draws = t.Draws ?? 0, LastPlayed = t.UpdatedAt, MatchesPlayed = (t.Wins ?? 0) + (t.Losses ?? 0) + (t.Draws ?? 0) }));
                        hasMorePages = !string.IsNullOrEmpty(apiData.NextPageUrl);
                    }
                    else
                    {
                        hasMorePages = false;
                    }
                }
                else // player
                {
                    var apiData = JsonSerializer.Deserialize<LeaderboardApiResponse>(json, options);
                    if (apiData != null && apiData.Leaderboard.Any())
                    {
                        allItems.AddRange(apiData.Leaderboard.Select(p => new LeaderboardDisplay { Id = p.DiscordId, Name = p.Name, Rank = p.Rank, AvatarDisplayString = p.AvatarDisplayString, ImageUrl = p.AvatarUrl, Elo = p.Elo, Wins = p.Wins, Losses = p.Losses, Draws = p.Draws, MatchesPlayed = p.MatchesPlayed, WinPercentage = p.WinPercentage, LastPlayed = p.LastPlayed, Gems = p.Gems, Xp = p.Xp, AchievementCount = p.AchievementCount }));
                        hasMorePages = !string.IsNullOrEmpty(apiData.NextPageUrl);
                    }
                    else
                    {
                        hasMorePages = false;
                    }
                }
                currentPage++;
            }

            return allItems;
        }

        private async Task FetchGamesAsync()
        {
            if (AllGames.Any()) return;

            var apiClient = _httpClientFactory.CreateClient("TfcEloApiClient");
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                return;
            }

            try
            {
                var response = await apiClient.GetAsync($"{apiBaseUrl}/api/games");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    AllGames = JsonSerializer.Deserialize<List<Game>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Game>();

                    // De-duplicate games based on shared ELO scope
                    var gameOptions = new List<SelectListItem>();
                    var addedScopes = new HashSet<string>();

                    // Per user, assume the base game definition (e.g. TFC) comes before regional ones (e.g. TFC_NA)
                    foreach (var game in AllGames)
                    {
                        var scopeId = game.EloSharedWithGameId ?? game.GameId;
                        if (!addedScopes.Contains(scopeId))
                        {
                            // Find the base game to get the display name.
                            var baseGame = AllGames.FirstOrDefault(g => g.GameId == scopeId);
                            var displayName = baseGame?.Name ?? scopeId;

                            gameOptions.Add(new SelectListItem { Value = scopeId, Text = displayName });
                            addedScopes.Add(scopeId);
                        }
                    }
                    GameOptions = gameOptions.OrderBy(g => g.Text).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while fetching games: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }
        
        public Dictionary<string, string> GetRouteData(int page)
        {
            return new Dictionary<string, string>
            {
                { "SelectedGameId", SelectedGameId },
                { "CurrentFilter", CurrentFilter },
                { "Metric", Metric },
                { "PageSize", PageSize.ToString() },
                { "NameLike", NameLike ?? "" },
                { "CurrentPage", page.ToString() }
            };
        }
        
        private void ProcessAndFilterItems(List<LeaderboardDisplay> allItems)
        {
            foreach(var item in allItems)
            {
                item.IsActive = item.LastPlayed.HasValue && (DateTime.UtcNow - item.LastPlayed.Value) <= TimeSpan.FromDays(30);
            }

            var activeItemsSorted = allItems
                .Where(p => p.IsActive == true)
                .OrderByDescending(p => p.Elo)
                .ToList();
            for (int i = 0; i < activeItemsSorted.Count; i++)
            {
                activeItemsSorted[i].ActiveRank = i + 1;
            }

            IEnumerable<LeaderboardDisplay> filteredItems = allItems;

            if (CurrentFilter == "active")
            {
                filteredItems = filteredItems.Where(p => p.IsActive == true);
            }

            if (!string.IsNullOrEmpty(NameLike))
            {
                filteredItems = filteredItems.Where(p => p.Name.Contains(NameLike, StringComparison.OrdinalIgnoreCase));
            }
            
            var finalQuery = filteredItems.AsQueryable();
            finalQuery = Metric.ToLower() switch
            {
                "wins" => finalQuery.OrderByDescending(p => p.Wins),
                "matches_played" => finalQuery.OrderByDescending(p => p.MatchesPlayed),
                "gems" => finalQuery.OrderByDescending(p => p.Gems),
                "xp" => finalQuery.OrderByDescending(p => p.Xp),
                "achievements" => finalQuery.OrderByDescending(p => p.AchievementCount),
                _ => finalQuery.OrderByDescending(p => p.Elo)
            };
            
            var finalItemList = finalQuery.ToList();

            TotalItems = finalItemList.Count;
            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
            CurrentPage = Math.Max(1, Math.Min(CurrentPage, TotalPages == 0 ? 1 : TotalPages));

            ItemsToDisplay = finalItemList
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
} 