using Microsoft.AspNetCore.Hosting;

namespace tfpugsdotcom.Pages
{
    public class AdminGuideModel : BaseContentPageModel
    {
        protected override string MarkdownFileName => "AdminGuide.md";
        protected override string PageTitle => "Admin Guide";

        public AdminGuideModel(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
} 