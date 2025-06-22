using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages
{
    public class TeamModel : PageModel
    {
        private readonly ILogger<TeamModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiTeam? TeamDetails { get; private set; }
        public string? ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int MatchHistoryCurrentPage { get; set; } = 1;

        public RecentMatchesApiResponse? MatchHistoryData { get; private set; }
        public string? MatchHistoryErrorMessage { get; private set; }

        public TeamModel(ILogger<TeamModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                ErrorMessage = "Team ID is required.";
                return;
            }

            var apiClient = _httpClientFactory.CreateClient();
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                return;
            }

            var teamDetailsTask = FetchTeamDetailsAsync(apiClient, apiBaseUrl);
            var matchHistoryTask = FetchMatchHistoryAsync(apiClient, apiBaseUrl);

            await Task.WhenAll(teamDetailsTask, matchHistoryTask);
        }

        private async Task FetchTeamDetailsAsync(HttpClient apiClient, string apiBaseUrl)
        {
            var requestUrl = $"{apiBaseUrl}/api/teams/{Id}";
            try
            {
                _logger.LogInformation("Fetching team details from: {RequestUrl}", requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    TeamDetails = JsonSerializer.Deserialize<ApiTeam>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ErrorMessage = $"A team with the ID '{Id}' could not be found.";
                    _logger.LogWarning(ErrorMessage);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching team data: {response.StatusCode}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }

        private async Task FetchMatchHistoryAsync(HttpClient apiClient, string apiBaseUrl)
        {
            var requestUrl = $"{apiBaseUrl}/api/matches?team_id={Id}&page={MatchHistoryCurrentPage}&page_size=10";
            try
            {
                _logger.LogInformation("Fetching team match history from: {RequestUrl}", requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    MatchHistoryData = JsonSerializer.Deserialize<RecentMatchesApiResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    MatchHistoryErrorMessage = $"Could not load match history (Status: {response.StatusCode}).";
                    _logger.LogWarning(MatchHistoryErrorMessage);
                }
            }
            catch (System.Exception ex)
            {
                MatchHistoryErrorMessage = "An error occurred while fetching the team's match history.";
                _logger.LogError(ex, MatchHistoryErrorMessage);
            }
        }
    }
} 