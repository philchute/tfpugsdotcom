using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_site.Pages.Servers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ConnectModel : PageModel
    {
        public IActionResult OnGet(string serverIdentifier)
        {
            if (string.IsNullOrEmpty(serverIdentifier))
            {
                return RedirectToPage("/Servers/Index");
            }

            switch (serverIdentifier.ToLowerInvariant())
            {
                case "dal":
                case "dallas":
                case "central":
                case "tfcentral":
                case "tfpcentral":
                case "tfpcentralvultr2":
                    return Redirect("steam://connect/149.28.241.217:27015/letsplay!");

                case "miami":
                case "southeast":
                case "tfsoutheast":
                case "tfpsoutheast":
                case "tfpsoutheastvultr":
                    return Redirect("steam://connect/45.77.162.42:27015/letsplay!");

                case "ny":
                case "nyc":
                case "newyork":
                case "oldnyc":
                case "oldnewyork":
                case "east":
                case "east2":
                case "tfeast":
                case "tfeast2":
                case "tfeast2vultr":
                case "tfpeast":
                case "tfpeast2":
                case "tfpeast2vultr":
                    return Redirect("steam://connect/104.207.129.123:27015/letsplay!");
                
                case "nnyc":
                case "newnyc":
                    return Redirect("steam://connect/149.28.56.141:27015/letsplay!");
                
                case "santiago":
                case "chile":
                case "tfpsantiago":
                case "tfpchile":
                    return Redirect("steam://connect/64.176.14.39:27015/letsplay!");

                case "ricochet":
                case "tfpugsricochet":
                    return Redirect("steam://connect/149.28.56.141:27016/letsplay!");

                default:
                    // If no match is found, redirect to the main servers list.
                    return RedirectToPage("/Servers/Index");
            }
        }
    }
} 