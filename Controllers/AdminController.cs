using Microsoft.AspNetCore.Mvc;

namespace PicturePilot.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/Admin/Reports")]
    public IActionResult Reports()
    {
        return View();
    }
    [HttpGet("/Admin/BlackListImages")]
    public IActionResult BlackListImages()
    {
        return View();
    }
    [HttpGet("/Admin/BlackListUsers")]
    public IActionResult BlackListUsers()
    {
        return View();
    }
}
