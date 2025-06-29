using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace tfpugsdotcom.Pages;

public class NewsItem
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("is_pinned")]
    public bool IsPinned { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public List<NewsItem> NewsItems { get; private set; } = new List<NewsItem>();

    public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task OnGet()
    {
        var newsFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Content", "news.json");
        if (System.IO.File.Exists(newsFilePath))
        {
            var jsonContent = await System.IO.File.ReadAllTextAsync(newsFilePath);
            var allNews = JsonSerializer.Deserialize<List<NewsItem>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (allNews != null)
            {
                NewsItems = allNews
                    .OrderByDescending(n => n.IsPinned)
                    .ThenByDescending(n => n.Timestamp)
                    .ToList();
            }
        }
    }
}
