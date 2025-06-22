using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tfpugsdotcom.Models;
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages.Races
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<RaceRecord> Records { get; private set; } = new List<RaceRecord>();
        public string? ErrorMessage { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            var apiClient = _httpClientFactory.CreateClient("TfcEloApiClient");
            var apiBaseUrl = _configuration["TfcEloApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                ErrorMessage = "API Base URL is not configured.";
                return;
            }

            var requestUrl = $"{apiBaseUrl}/api/races/leaderboard";
            try
            {
                _logger.LogInformation("Fetching race records from: {RequestUrl}", requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Records = JsonSerializer.Deserialize<List<RaceRecord>>(jsonResponse, options) ?? new List<RaceRecord>();
                }
                else
                {
                    ErrorMessage = $"Error fetching data from API: {response.StatusCode}";
                    _logger.LogWarning("API request to {RequestUrl} failed with status code {StatusCode}", requestUrl, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                _logger.LogError(ex, "Exception while fetching race records from {RequestUrl}", requestUrl);
            }
        }
    }
} 