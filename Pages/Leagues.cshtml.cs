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
    public class LeaguesModel : PageModel
    {
        private readonly ILogger<LeaguesModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // Data for the page
        public List<ApiLeague> AllLeagues { get; private set; } = new List<ApiLeague>();
        public ApiLeagueDetails? SelectedLeagueDetails { get; private set; }
        public string? ErrorMessage { get; private set; }

        // Dropdown options
        public List<SelectListItem> LeagueOptions { get; private set; } = new List<SelectListItem>();

        [BindProperty(SupportsGet = true)]
        public string? SelectedLeagueId { get; set; }

        public LeaguesModel(ILogger<LeaguesModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                return;
            }

            // Step 1: Fetch all leagues to populate the dropdown
            await FetchAllLeagues(apiClient, apiBaseUrl);

            // Step 2: If a league is selected (or if it's the first visit and we have leagues), fetch its details
            if (!string.IsNullOrEmpty(SelectedLeagueId))
            {
                await FetchLeagueDetails(apiClient, apiBaseUrl, SelectedLeagueId);
            }
            else if (AllLeagues.Any())
            {
                // If no league is selected, default to showing the most recent one.
                var defaultLeagueId = AllLeagues.OrderByDescending(l => l.CreatedAt).First().LeagueId;
                SelectedLeagueId = defaultLeagueId;
                await FetchLeagueDetails(apiClient, apiBaseUrl, defaultLeagueId);
            }
        }

        private async Task FetchAllLeagues(HttpClient apiClient, string apiBaseUrl)
        {
            try
            {
                var requestUrl = $"{apiBaseUrl}/api/leagues";
                _logger.LogInformation("Fetching all leagues from {RequestUrl}", requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    AllLeagues = JsonSerializer.Deserialize<List<ApiLeague>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ApiLeague>();
                    
                    // Populate dropdown options
                    LeagueOptions = AllLeagues
                        .OrderByDescending(l => l.CreatedAt)
                        .Select(l => new SelectListItem { Value = l.LeagueId, Text = l.Name })
                        .ToList();
                }
                else
                {
                    ErrorMessage = $"Error fetching league list: {response.StatusCode}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred while fetching leagues: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }

        private async Task FetchLeagueDetails(HttpClient apiClient, string apiBaseUrl, string leagueId)
        {
            try
            {
                var requestUrl = $"{apiBaseUrl}/api/leagues/{leagueId}";
                _logger.LogInformation("Fetching details for league {LeagueId} from {RequestUrl}", leagueId, requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    SelectedLeagueDetails = JsonSerializer.Deserialize<ApiLeagueDetails>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ErrorMessage = $"League with ID '{leagueId}' was not found.";
                     _logger.LogWarning(ErrorMessage);
                }
                else
                {
                    ErrorMessage = $"Error fetching league details: {response.StatusCode}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred while fetching league details: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }
    }
} 