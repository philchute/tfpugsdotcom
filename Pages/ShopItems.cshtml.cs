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
using tfpugsdotcom.Models.ApiResponses;

namespace tfpugsdotcom.Pages
{
    public class ShopItemsModel : PageModel
    {
        private readonly ILogger<ShopItemsModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<ShopItem> ShopItems { get; private set; } = new List<ShopItem>();
        public string? ErrorMessage { get; private set; }

        public ShopItemsModel(ILogger<ShopItemsModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                _logger.LogError(ErrorMessage);
                ShopItems = new List<ShopItem>();
                return;
            }

            try
            {
                var response = await apiClient.GetAsync($"{apiBaseUrl}/api/shop/items");

                if (response != null && response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var allItems = JsonSerializer.Deserialize<List<ShopItem>>(json, options);
                    
                    if (allItems != null)
                    {
                        ShopItems = allItems;
                    }
                }
                else
                {
                    // Handle error or empty state
                    var errorContent = response != null ? await response.Content.ReadAsStringAsync() : "Response was null";
                    ErrorMessage = $"Error fetching shop items: {response?.StatusCode}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                    ShopItems = new List<ShopItem>();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                ShopItems = new List<ShopItem>();
            }
        }

        public string GetAvailabilityText(string? availability)
        {
            string friendlyText = availability?.ToLower() switch
            {
                "pre_match_start" => "pre-match in your PUG channel",
                "anytime" => "anytime",
                _ => availability ?? "Not specified"
            };

            return friendlyText;
        }
    }
} 