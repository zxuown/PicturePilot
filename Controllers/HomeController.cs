using Microsoft.AspNetCore.Mvc;

namespace PicturePilot.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
