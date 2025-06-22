using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MatchDetailsApiResponse? MatchDetailsData { get; private set; }
        public string? ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty; // Match Identifier from route

        public DetailsModel(ILogger<DetailsModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                ErrorMessage = "Match Identifier is required.";
                _logger.LogError(ErrorMessage);
                return;
            }

            var apiClient = _httpClientFactory.CreateClient();
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                _logger.LogError(ErrorMessage);
                return;
            }

            var apiUrl = $"{apiBaseUrl}/api/match/{Id}";

            try
            {
                _logger.LogInformation("Fetching match details from: {ApiUrl}", apiUrl);
                var response = await apiClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    MatchDetailsData = JsonSerializer.Deserialize<MatchDetailsApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    _logger.LogInformation("Successfully fetched match details for identifier {MatchId}", Id);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ErrorMessage = $"Match with identifier '{Id}' not found.";
                    _logger.LogWarning(ErrorMessage);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching match details: {response.StatusCode}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HttpRequestException occurred while fetching match details for {MatchId}: {OriginalMessage}", Id, ex.Message);
                ErrorMessage = "The TFC Pugs ranking service is currently unavailable. Please try again later.";
                MatchDetailsData = null; // Ensure model is in a safe state
            }
            catch (JsonException ex)
            {
                ErrorMessage = $"Error deserializing match details response: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred while fetching match details: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }
    }
} 