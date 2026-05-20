using Microsoft.AspNetCore.Mvc;

namespace BookMarketplace.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}