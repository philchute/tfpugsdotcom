using Microsoft.AspNetCore.Hosting;

namespace tfpugsdotcom.Pages
{
    public class RunnersGuideModel : BaseContentPageModel
    {
        protected override string MarkdownFileName => "RunnersGuide.md";
        protected override string PageTitle => "Runner's Guide";

        public RunnersGuideModel(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
} 