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
    public class BracketParticipantViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }

    public class TournamentsModel : PageModel
    {
        private readonly ILogger<TournamentsModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public string ErrorMessage { get; private set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }

        public List<Tournament> Tournaments { get; set; } = new();
        public Tournament? TournamentDetails { get; set; }
        public List<SelectListItem> TournamentOptions { get; set; } = new();

        public string BracketDataJson { get; set; } = "{}";
        public string MatchesStructureJson { get; set; } = "[]";

        public TournamentsModel(ILogger<TournamentsModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            try
            {
                var tournamentsUrl = $"{apiBaseUrl}/api/tournaments?page={CurrentPage}&page_size=5"; // page_size=5 for example
                var tournamentsResponse = await apiClient.GetAsync(tournamentsUrl);

                if (tournamentsResponse.IsSuccessStatusCode)
                {
                    var json = await tournamentsResponse.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<TournamentsApiResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiResponse != null)
                    {
                        Tournaments = apiResponse.Tournaments ?? new List<Tournament>();
                        TotalPages = apiResponse.TotalPages;
                        CurrentPage = apiResponse.CurrentPage;
                    }
                }
                else
                {
                    _logger.LogError("Failed to fetch tournaments from API. Status: {StatusCode}", tournamentsResponse.StatusCode);
                    ErrorMessage = "Could not load tournament list.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing tournament data.");
                ErrorMessage = "An error occurred while fetching tournament data. Please try again later.";
            }
        }
    }
} 