using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
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

    [HttpGet("/Home/{query?}")]
    public async Task<IActionResult> Home(SearchModel? search)
    {
            var images = await _imageRepository.SearchAsync(search);
        ViewData["Tags"] = _imageRepository.GetTags().ToList();
            return View(images);
    }
}
