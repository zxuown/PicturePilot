using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

[Authorize]
public class ReportsController(ReportRepository reportRepository, UserManager<User> userManager, ImageRepository imageRepository) : Controller
{
    private readonly ReportRepository _reportRepository = reportRepository;

    private readonly UserManager<User> _userManager = userManager;

    private readonly ImageRepository _imageRepository = imageRepository;

    [HttpGet("/Reports/Create/{targetId}")]
    public async Task<IActionResult> Create(int targetId)
    {
        return View(new Report
        {
            TargetId = targetId,
        });
    }

    [HttpPost("/Reports/Create/{targetId}")]
    public async Task<IActionResult> Create([FromForm] Report report, int targetId)
    {
        Image image = await _imageRepository.GetByIdAsync(targetId);
        if ((int)report.ReportType == 0)
        {
            User user = await _userManager.FindByIdAsync(image.UserId.ToString());
            report.TargetId = user.Id;
        }
        else
        {
            report.TargetId = targetId;
        }
        report.Sender = await _userManager.GetUserAsync(User);
        await _reportRepository.AddAsync(report);
        return Ok();
    }
}
