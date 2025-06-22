using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages.Races
{
    public class MapModel : PageModel
    {
        private readonly ILogger<MapModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public string MapName { get; set; } = string.Empty;

        public List<RaceMapLeaderboardEntry> Leaderboard { get; private set; } = new();
        public string? ErrorMessage { get; private set; }

        public MapModel(ILogger<MapModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(MapName))
            {
                ErrorMessage = "Map name is required.";
                return;
            }

            var apiClient = _httpClientFactory.CreateClient("TfcEloApiClient");
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                return;
            }
            
            try
            {
                var requestUrl = $"{apiBaseUrl}/api/racemaps/{MapName}/leaderboard";
                var response = await apiClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Leaderboard = JsonSerializer.Deserialize<List<RaceMapLeaderboardEntry>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ErrorMessage = $"No race times found for map: {MapName}";
                }
                else
                {
                    ErrorMessage = $"Error fetching leaderboard: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, "Error fetching race map leaderboard for {MapName}", MapName);
            }
        }
    }
} 