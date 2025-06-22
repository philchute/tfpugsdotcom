using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration; // For IConfiguration
using Microsoft.Extensions.Logging;     // For ILogger
using System.Net.Http;                  // For HttpClient, IHttpClientFactory
using System.Text.Json;                 // For JsonSerializer
using System.Threading.Tasks;           // For Task
using System.Collections.Generic;       // For List
using tfpugsdotcom.Models.ApiResponses;              // Where ApiGameServer and ApiLiveMatch are defined

namespace tfpugsdotcom.Pages.Servers // Assuming 'tfpugsdotcom' is your main project namespace
{
    public class IndexModel : PageModel
    {
        // Dependencies
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;

        // Properties to hold data for the Razor page
        public List<ApiGameServer> AllServers { get; private set; } = new List<ApiGameServer>();
        public List<ApiLiveMatch> LiveMatches { get; private set; } = new List<ApiLiveMatch>();
        public string? ErrorMessage { get; private set; }

        // Constructor
        public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        // OnGetAsync method
        public async Task OnGetAsync()
        {
            var apiClient = _httpClientFactory.CreateClient("TfcEloApi"); // Use a named HttpClient if configured
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];

            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL (TfcEloApiBaseUrl) is not configured in appsettings.json.";
                _logger.LogError(ErrorMessage);
                return;
            }

            // Fetch All Servers
            try
            {
                var serversRequestUrl = $"{apiBaseUrl.TrimEnd('/')}/api/servers";
                _logger.LogInformation("Fetching server list from: {Url}", serversRequestUrl);
                var serversResponse = await apiClient.GetAsync(serversRequestUrl);

                if (serversResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await serversResponse.Content.ReadAsStringAsync();
                    AllServers = JsonSerializer.Deserialize<List<ApiGameServer>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ApiGameServer>();
                    _logger.LogInformation("Successfully fetched and deserialized server list. Count: {Count}", AllServers.Count);
                }
                else
                {
                    var errorContent = await serversResponse.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching server list: {serversResponse.StatusCode}. URL: {serversRequestUrl}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while fetching the server list.";
                _logger.LogError(ex, ErrorMessage);
            }

            // Fetch Live Matches
            try
            {
                var liveMatchesRequestUrl = $"{apiBaseUrl.TrimEnd('/')}/api/matches/live";
                _logger.LogInformation("Fetching live matches from: {Url}", liveMatchesRequestUrl);
                var liveMatchesResponse = await apiClient.GetAsync(liveMatchesRequestUrl);

                if (liveMatchesResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await liveMatchesResponse.Content.ReadAsStringAsync();
                    LiveMatches = JsonSerializer.Deserialize<List<ApiLiveMatch>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ApiLiveMatch>();
                    _logger.LogInformation("Successfully fetched and deserialized live matches. Count: {Count}", LiveMatches.Count);
                }
                else
                {
                    var errorContent = await liveMatchesResponse.Content.ReadAsStringAsync();
                    var liveMatchError = $"Error fetching live matches: {liveMatchesResponse.StatusCode}. URL: {liveMatchesRequestUrl}. Details: {errorContent}";
                    _logger.LogError(liveMatchError);
                    if (string.IsNullOrEmpty(ErrorMessage)) ErrorMessage = liveMatchError;
                    else ErrorMessage += " \n| " + liveMatchError; // Append with a newline for better readability if both fail
                }
            }
            catch (Exception ex)
            {
                var liveMatchError = "An error occurred while fetching live matches.";
                _logger.LogError(ex, liveMatchError);
                if (string.IsNullOrEmpty(ErrorMessage)) ErrorMessage = liveMatchError;
                else ErrorMessage += " \n| " + liveMatchError;
            }
        }
    }
} 