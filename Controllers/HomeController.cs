using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class HomeController(ImageRepository imageRepository) : Controller
{
    private readonly ImageRepository _imageRepository = imageRepository;
    [HttpGet("/")]
    public IActionResult Index()
    {
        if (User.IsInRole("ADMIN"))
        {
            return Redirect("/Admin");
        }
        else
        {
            return Redirect("/Home");
        }
    }


    [HttpGet("/Home")]
    public async Task<IActionResult> Home()
    {
        var images = await _imageRepository.GetAllAsync();
        return View(images);
    }
}
