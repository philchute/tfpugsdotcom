using Microsoft.AspNetCore.Hosting;

namespace tfpugsdotcom.Pages
{
    public class ServerGuideModel : BaseContentPageModel
    {
        protected override string MarkdownFileName => "ServerGuide.md";
        protected override string PageTitle => "Server Guide";

        public ServerGuideModel(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
} 