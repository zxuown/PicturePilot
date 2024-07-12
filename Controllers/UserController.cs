using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

[Authorize]
public class UserController(UserRepository userRepository, UserManager<User> userManager) : Controller
{ 
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<User> _userManager = userManager;

    [HttpGet("/History")]
    public async Task<IActionResult> History()
    {
        var userId = _userManager.GetUserId(User);
        var history = await _userRepository.GetHistoryAsync(int.Parse(userId));
        return View(history);
    }

    [HttpPost("/History/Clear")]
    public async Task<IActionResult> ClearHistory(string timeframe)
    {
        var userId = _userManager.GetUserId(User);
        DateTime cutoffDate = timeframe switch
        {
            "1day" => DateTime.Now.AddDays(-1),
            "1week" => DateTime.Now.AddDays(-7),
            "1month" => DateTime.Now.AddMonths(-1),
            "1year" => DateTime.Now.AddYears(-1),
            "all" => DateTime.MinValue,
            _ => DateTime.Now
        };

        await _userRepository.ClearHistory(int.Parse(userId), cutoffDate);
        return RedirectToAction("History");
    }

    [HttpGet("/Favorites")]
    [Authorize]
    public async Task<IActionResult> Favorites()
    {
        var userId = _userManager.GetUserId(User);
        var favorites = await _userRepository.GetFavoritesAsync(int.Parse(userId));
        return View(favorites);
    }

    [HttpGet("/Profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        return Redirect($"/View/User/{user.Id}");
    }
}
