using Microsoft.AspNetCore.Mvc;

namespace PicturePilot.Controllers;

public class ImageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
