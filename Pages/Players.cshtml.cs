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
using Microsoft.AspNetCore.Mvc.Rendering;
using tfpugsdotcom.Models; // Your models namespace
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages
{
    public class PlayersModel : PageModel // Changed from PlayerProfileModel to PlayersModel for /Players/{id} route
    {
        private readonly ILogger<PlayersModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PlayerProfileApiResponse? ProfileData { get; private set; }
        public RecentMatchesApiResponse? MatchHistoryData { get; private set; } // Reusing for player's matches
        public Dictionary<string, EloHistoryApiResponse?> EloHistories { get; private set; } = new Dictionary<string, EloHistoryApiResponse?>();
        public string? ErrorMessage { get; private set; }
        public string? ProfileErrorMessage { get; private set; }
        public string? MatchHistoryErrorMessage { get; private set; }
        public Dictionary<string, string?> EloHistoryErrorMessages { get; private set; } = new Dictionary<string, string?>();


        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty; // Discord ID from route

        [BindProperty(SupportsGet = true)]
        public int MatchHistoryCurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GameId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MapNameLike { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ServerNameLike { get; set; }

        public List<SelectListItem> GameIdOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Any Game" },
            new SelectListItem { Value = "TFC", Text = "TFC" },
            new SelectListItem { Value = "QWTF", Text = "QWTF" }
        };

        public PlayersModel(ILogger<PlayersModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                ErrorMessage = "Player Discord ID is required.";
                _logger.LogError(ErrorMessage);
                return;
            }

            var apiClient = _httpClientFactory.CreateClient("TfcEloApiClient");
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                _logger.LogError(ErrorMessage);
                return;
            }

            // Task 1: Fetch Core Profile Data
            var profileUrl = $"{apiBaseUrl}/api/player/{Id}/profile";
            try
            {
                _logger.LogInformation("Fetching player profile from: {ProfileUrl}", profileUrl);
                var profileResponse = await apiClient.GetAsync(profileUrl);
                if (profileResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await profileResponse.Content.ReadAsStringAsync();
                    ProfileData = JsonSerializer.Deserialize<PlayerProfileApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    _logger.LogInformation("Successfully fetched player profile for {PlayerId}", Id);

                    // Task 2 (dependent): Fetch ELO Histories for each game in profile
                    if (ProfileData?.GameSpecificStats != null)
                    {
                        foreach (var gameStats in ProfileData.GameSpecificStats)
                        {
                            var eloHistoryUrl = $"{apiBaseUrl}/api/player/{Id}/elo_history/{gameStats.GameId}";
                            try
                            {
                                _logger.LogInformation("Fetching ELO history for game {GameId} from: {EloHistoryUrl}", gameStats.GameId, eloHistoryUrl);
                                var eloResponse = await apiClient.GetAsync(eloHistoryUrl);
                                if (eloResponse.IsSuccessStatusCode)
                                {
                                    var eloJson = await eloResponse.Content.ReadAsStringAsync();
                                    EloHistories[gameStats.GameId] = JsonSerializer.Deserialize<EloHistoryApiResponse>(eloJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                }
                                else
                                {
                                    var eloErrorContent = await eloResponse.Content.ReadAsStringAsync();
                                    EloHistoryErrorMessages[gameStats.GameId] = $"Error fetching ELO history for {gameStats.GameId}: {eloResponse.StatusCode}. Details: {eloErrorContent}";
                                    _logger.LogError(EloHistoryErrorMessages[gameStats.GameId]);
                                }
                            }
                            catch (Exception ex)
                            {
                                EloHistoryErrorMessages[gameStats.GameId] = $"Exception fetching ELO history for {gameStats.GameId}: {ex.Message}";
                                _logger.LogError(ex, EloHistoryErrorMessages[gameStats.GameId]);
                            }
                        }
                    }
                }
                else if (profileResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ProfileErrorMessage = $"Player with Discord ID {Id} not found.";
                    _logger.LogWarning(ProfileErrorMessage);
                }
                else
                {
                    var errorContent = await profileResponse.Content.ReadAsStringAsync();
                    ProfileErrorMessage = $"Error fetching player profile: {profileResponse.StatusCode}. Details: {errorContent}";
                    _logger.LogError(ProfileErrorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HttpRequestException occurred while fetching player profile for {PlayerId}: {OriginalMessage}", Id, ex.Message);
                ErrorMessage = "The TFC Pugs ranking service is currently unavailable. Please try again later.";
                ProfileData = null;
                EloHistories = new Dictionary<string, EloHistoryApiResponse?>();
            }
            catch (JsonException ex)
            {
                ProfileErrorMessage = $"JSON exception fetching player profile: {ex.Message}";
                _logger.LogError(ex, ProfileErrorMessage);
            }

            // Task 3: Fetch Match History
            var matchHistoryParams = new Dictionary<string, string>
            {
                { "page", MatchHistoryCurrentPage.ToString() },
                { "player_id", Id }
            };
            if (StartDate.HasValue) matchHistoryParams.Add("start_date", StartDate.Value.ToString("yyyy-MM-dd"));
            if (EndDate.HasValue) matchHistoryParams.Add("end_date", EndDate.Value.ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(GameId)) matchHistoryParams.Add("game_id", GameId);
            if (!string.IsNullOrEmpty(MapNameLike)) matchHistoryParams.Add("map_name_like", MapNameLike);
            if (!string.IsNullOrEmpty(ServerNameLike)) matchHistoryParams.Add("server_name_like", ServerNameLike);
            
            var matchHistoryQueryString = string.Join("&", matchHistoryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            var matchHistoryUrl = $"{apiBaseUrl}/api/matches?{matchHistoryQueryString}";

            try
            {
                _logger.LogInformation("Fetching match history from: {MatchHistoryUrl}", matchHistoryUrl);
                var matchesResponse = await apiClient.GetAsync(matchHistoryUrl);
                if (matchesResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await matchesResponse.Content.ReadAsStringAsync();
                    MatchHistoryData = JsonSerializer.Deserialize<RecentMatchesApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    _logger.LogInformation("Successfully fetched match history for player {PlayerId}, page {Page}", Id, MatchHistoryCurrentPage);
                }
                else
                {
                    var errorContent = await matchesResponse.Content.ReadAsStringAsync();
                    MatchHistoryErrorMessage = $"Error fetching match history: {matchesResponse.StatusCode}. Details: {errorContent}";
                    _logger.LogError(MatchHistoryErrorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HttpRequestException occurred while fetching match history for player {PlayerId}: {OriginalMessage}", Id, ex.Message);
                MatchHistoryErrorMessage = "Match history is temporarily unavailable due to a service issue.";
                MatchHistoryData = null;
            }
            catch (Exception ex)
            {
                MatchHistoryErrorMessage = $"Exception fetching match history: {ex.Message}";
                _logger.LogError(ex, MatchHistoryErrorMessage);
                MatchHistoryData = null;
            }
            
            // Consolidate error messages if all main fetches failed for some reason
            if (ProfileData == null && string.IsNullOrEmpty(ErrorMessage))
            {
                if (!string.IsNullOrEmpty(ProfileErrorMessage)) ErrorMessage = ProfileErrorMessage;
            }
            if (ProfileData != null && !string.IsNullOrEmpty(MatchHistoryErrorMessage) && MatchHistoryErrorMessage.Contains("service issue"))
            {
                // If profile is fine, but match history is down due to API, show specific error for match history section,
                // but not as the main page ErrorMessage if profile loaded.
                // The current MatchHistoryErrorMessage will be displayed in its section.
            }
            else if (ProfileData == null && MatchHistoryData == null && string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "Player data could not be loaded at this time.";
            }
        }
    }
} 