using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class UserController : Controller
{ 
    private readonly UserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    [HttpGet("/history")]
    public async Task<IActionResult> History()
    {
        var userId = _userManager.GetUserId(User);
        var history = await _userRepository.GetHistoryAsync(int.Parse(userId));
        return View(history);
    }

    [HttpPost("/history/clear")]
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
}
