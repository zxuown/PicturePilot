using Microsoft.AspNetCore.Mvc;

namespace PicturePilot.Controllers;

public class TagController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
