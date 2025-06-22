using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using tfpugsdotcom.Models; // Assuming your models are in this namespace
using tfpugsdotcom.Models.ApiResponses;
using System.Collections.Generic; // Added for List
using Microsoft.AspNetCore.Mvc.Rendering; // Added for SelectListItem
using System.Linq; // Added for Enumerable.Any validation

namespace tfpugsdotcom.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RecentMatchesApiResponse? ApiData { get; private set; }
        public string? ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 20; // Default API page_size

        [BindProperty(SupportsGet = true)]
        public string? GameId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MapNameLike { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ServerNameLike { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PlayerNameLike { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        // Unused SubmittedGameId removed for now, page reset logic simplified

        public List<SelectListItem> PageSizeOptions { get; }
            = new List<SelectListItem>
            {
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "20", Text = "20" },
                new SelectListItem { Value = "50", Text = "50" },
                new SelectListItem { Value = "100", Text = "100" },
            };

        // GameIdOptions will be populated from GameDataService in constructor or OnGet
        public List<SelectListItem> GameIdOptions { get; private set; } = new List<SelectListItem>();

        // For the "Go to Page" input
        [BindProperty(SupportsGet = true)] // Also bind from GET for direct navigation
        public int? GoToPage { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                _logger.LogError(ErrorMessage);
                return;
            }

            // This now happens first to populate the filter
            await FetchGamesForFilter(apiClient, apiBaseUrl);

            // If any filter is applied via query string and it's not a direct GoToPage action, reset to page 1.
            bool hasFiltersInQuery = Request.Query.ContainsKey(nameof(GameId)) || 
                                     Request.Query.ContainsKey(nameof(MapNameLike)) || 
                                     Request.Query.ContainsKey(nameof(ServerNameLike)) ||
                                     Request.Query.ContainsKey(nameof(PlayerNameLike)) ||
                                     Request.Query.ContainsKey(nameof(StartDate)) ||
                                     Request.Query.ContainsKey(nameof(EndDate));

            if (hasFiltersInQuery && !GoToPage.HasValue && !Request.Query.ContainsKey(nameof(CurrentPage))) 
            {
                 CurrentPage = 1;
            }
            
            if (GoToPage.HasValue && GoToPage > 0)
            {
                CurrentPage = GoToPage.Value;
            }
            else if (!hasFiltersInQuery && Request.Query.TryGetValue(nameof(CurrentPage), out var pageFromQuery))
            {
                // This handles direct pagination link clicks where CurrentPage is in the query
                if (int.TryParse(pageFromQuery, out int parsedPageFromQuery) && parsedPageFromQuery > 0)
                {
                    CurrentPage = parsedPageFromQuery;
                }
            }

            if (!PageSizeOptions.Any(pso => pso.Value == PageSize.ToString()))
            {
                PageSize = 20; // Default to 20 if invalid
            }

            var queryParams = new Dictionary<string, string>();
            queryParams.Add("page", Math.Max(1, CurrentPage).ToString());
            queryParams.Add("page_size", PageSize.ToString());

            if (!string.IsNullOrEmpty(GameId)) queryParams.Add("game_id", GameId);
            if (!string.IsNullOrEmpty(MapNameLike)) queryParams.Add("map_name_like", MapNameLike);
            if (!string.IsNullOrEmpty(ServerNameLike)) queryParams.Add("server_name_like", ServerNameLike);
            if (!string.IsNullOrEmpty(PlayerNameLike)) queryParams.Add("player_name_like", PlayerNameLike);
            if (StartDate.HasValue) queryParams.Add("start_date", StartDate.Value.ToString("yyyy-MM-dd"));
            if (EndDate.HasValue) queryParams.Add("end_date", EndDate.Value.ToString("yyyy-MM-dd"));

            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            var requestUrl = $"{apiBaseUrl}/api/matches?{queryString}";

            try
            {
                _logger.LogInformation("Fetching recent matches from: {RequestUrl}", requestUrl);
                var response = await apiClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    ApiData = JsonSerializer.Deserialize<RecentMatchesApiResponse>(jsonResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (ApiData != null && ApiData.TotalPages > 0 && CurrentPage > ApiData.TotalPages)
                    {
                        CurrentPage = ApiData.TotalPages;
                        // Consider re-fetching if CurrentPage was adjusted significantly, or handle gracefully in UI
                    }
                    if (ApiData != null && ApiData.TotalItems == 0 && CurrentPage > 1)
                    { // If no items found on a page > 1, might mean filters yield no results for this page.
                      // Optionally redirect to page 1 with same filters, or just let it display "No matches"
                        _logger.LogInformation("No matches found on page {CurrentPage} with current filters. Total items: 0.", CurrentPage);
                    }
                    _logger.LogInformation("Successfully fetched and deserialized recent matches.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error fetching data from API: {response.StatusCode} - {response.ReasonPhrase}. Details: {errorContent}";
                    _logger.LogError(ErrorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HttpRequestException occurred while fetching recent matches: {OriginalMessage}", ex.Message);
                ErrorMessage = "The TFC Pugs ranking service is currently unavailable. Please try again later.";
                ApiData = new RecentMatchesApiResponse(); // Ensure ApiData is not null and has default/empty values
            }
            catch (JsonException ex)
            {
                ErrorMessage = $"JSON deserialization error: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
            catch (Exception ex) // Catch-all for other unexpected errors
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }
        }

        private async Task FetchGamesForFilter(HttpClient apiClient, string apiBaseUrl)
        {
            // Always add the default option first
            GameIdOptions.Add(new SelectListItem { Value = "", Text = "All Games" });

            try
            {
                var gamesUrl = $"{apiBaseUrl}/api/games";
                _logger.LogInformation("Fetching game list for filter from {GamesUrl}", gamesUrl);
                var gamesResponse = await apiClient.GetAsync(gamesUrl);

                if (gamesResponse.IsSuccessStatusCode)
                {
                    var gamesJson = await gamesResponse.Content.ReadAsStringAsync();
                    // We can reuse the Game model here
                    var allGames = JsonSerializer.Deserialize<List<Game>>(gamesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (allGames != null)
                    {
                        // For recent matches, we list all playable games, not just tournament ones.
                        GameIdOptions.AddRange(allGames.Select(g => new SelectListItem { Value = g.GameId, Text = g.Name }));
                    }
                }
                else
                {
                    _logger.LogWarning("Could not fetch game list for filter dropdown. API returned {StatusCode}", gamesResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch games for filter dropdown.");
            }
        }
    }
} 