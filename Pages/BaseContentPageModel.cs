using Markdig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace tfpugsdotcom.Pages
{
    public abstract class BaseContentPageModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public string? ContentAsHtml { get; private set; }
        public string? ErrorMessage { get; private set; }
        protected abstract string MarkdownFileName { get; }
        protected abstract string PageTitle { get; }


        public BaseContentPageModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Title"] = PageTitle;
            var markdownFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Content", MarkdownFileName);

            if (!System.IO.File.Exists(markdownFilePath))
            {
                ErrorMessage = $"The {PageTitle} file could not be found. Please check the 'Content' directory.";
                return Page();
            }

            try
            {
                var markdownContent = await System.IO.File.ReadAllTextAsync(markdownFilePath);
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                ContentAsHtml = Markdown.ToHtml(markdownContent, pipeline);
            }
            catch (IOException ex)
            {
                ErrorMessage = $"An error occurred while reading the {PageTitle} file: {ex.Message}";
            }

            return Page();
        }
    }
} 