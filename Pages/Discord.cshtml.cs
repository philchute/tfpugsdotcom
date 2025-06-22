using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_site.Pages
{
    public class DiscordModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirect to the Discord server invite link
            return Redirect("https://discord.gg/MmZeyPUH6q");
        }
    }
} 