using Microsoft.AspNetCore.Hosting;

namespace tfpugsdotcom.Pages
{
    public class PlayerGuideModel : BaseContentPageModel
    {
        protected override string MarkdownFileName => "PlayerInfo.md";
        protected override string PageTitle => "Player Guide";

        public PlayerGuideModel(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
    }
} 