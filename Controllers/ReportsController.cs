using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Enums;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

[Authorize]
public class ReportsController(ReportRepository reportRepository, UserManager<User> userManager, ImageRepository imageRepository) : Controller
{
    private readonly ReportRepository _reportRepository = reportRepository;

    private readonly UserManager<User> _userManager = userManager;

    private readonly ImageRepository _imageRepository = imageRepository;

    [HttpGet("/Reports/Create/{type}/{targetId}")]
    public async Task<IActionResult> Create(ReportType type, int targetId)
    {
        return View(new Report
        {
            TargetId = targetId,
            ReportType = (int)type == 0 ? ReportType.User : ReportType.Image
        });
    }

    [HttpPost("/Reports/Create/{type}/{targetId}")]
    public async Task<IActionResult> Create([FromForm] Report report, ReportType type, int targetId)
    {
        report.ReportType = (int)type == 0 ? ReportType.User : ReportType.Image;
        report.TargetId = targetId;
        report.Sender = await _userManager.GetUserAsync(User);
        await _reportRepository.AddAsync(report);
        return Ok();
    }
}
