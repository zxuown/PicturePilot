using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class ViewController(UserRepository userRepository, ImageRepository imageRepository, UserManager<User> userManager) : Controller
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly UserManager<User> _userManager = userManager;

    [HttpGet("/View/User/{id}")]
    public async Task<IActionResult> ViewUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return View(user);
    }

    [HttpGet("/View/Image/{id}")]
    public async Task<IActionResult> ViewImage(int id)
    {
        var image = await _imageRepository.GetByIdAsync(id);
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            _userRepository.AddToHistory(user.Id, id);
        }
        return View(new ImageModel
        {
            Image = image,
            IsFavorite = _userRepository.IsFavorite(user.Id, image.Id)
        });
    }
}
