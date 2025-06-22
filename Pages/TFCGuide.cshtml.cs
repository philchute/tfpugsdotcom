using Microsoft.AspNetCore.Hosting;

namespace tfpugsdotcom.Pages
{
    public class TFCGuideModel : BaseContentPageModel
    {
        protected override string MarkdownFileName => "TFCGuide.md";
        protected override string PageTitle => "TFC Guide";

        public TFCGuideModel(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
} 