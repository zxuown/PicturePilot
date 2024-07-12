using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class HomeController(ImageRepository imageRepository, UserManager<User> userManager) : Controller
{
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly UserManager<User> _userManager = userManager;
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
        var user = await _userManager.GetUserAsync(User);
        if (search != null && user != null)
        {
            search.UserId = user.Id;
        }
        var images = await _imageRepository.SearchAsync(search);
        ViewData["Tags"] = _imageRepository.GetTags().ToList();
        return View(images);
    }
}
