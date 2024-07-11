using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Business.Services;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class ImageController(ImageService imageService, ImageRepository imageRepository, UserRepository userRepository, UserManager<User> userManager) : Controller
{
    private readonly ImageService _imageService = imageService;
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<User> _userManager = userManager;

    [Authorize]
    [HttpGet("/images/add")]
    public IActionResult AddImage()
    {
        return View();
    }

    [Authorize]
    [HttpPost("/images/add")]
    public async Task<IActionResult> AddImage(ImageUploadViewModel model)
    {
        if (ModelState.IsValid)
        {
            var url = await _imageService.Upload(model.ImageFile);
            var analysis = await _imageService.AnalyzeImageAsync(url);
            var isContentAppropriate = !analysis.Adult.IsAdultContent;
            if (!isContentAppropriate)
            {
                return BadRequest(new { Error = "Image contains adult content." });
            }
            var image = new Image
            {
                Title = model.Title,
                Url = url,
                IsBLocked = !isContentAppropriate,
                UserId = (await _userManager.GetUserAsync(User)).Id,
                CreatedAt = DateTime.Now,
            };
            await _imageRepository.AddAsync(image);
            return RedirectToAction("SuccessPage");
        }

        return View(model);
    }

    [HttpGet("/images/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var image = await _imageRepository.GetByIdAsync(id);

        if (user != null)
        {
            _userRepository.AddToHistory(user.Id, id);
        }
        return View(image);
    }
}
