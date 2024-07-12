using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

[Authorize]
public class ReportsController(ReportRepository reportRepository, UserManager<User> userManager) : Controller
{
    private readonly ReportRepository _reportRepository = reportRepository;

    private readonly UserManager<User> _userManager = userManager;

    [HttpGet("/Reports/Create/{targetId}")]
    public async Task<IActionResult> Create(int id)
    {
        return View(new Report
        {
            TargetId = id,
            Sender = await _userManager.GetUserAsync(User)
        });
    }

    [HttpPost("/Reports/Create")]
    public async Task<IActionResult> Create([FromForm] Report report)
    {
        await _reportRepository.AddAsync(report);
        return Redirect($"/Reports/Create/{report.TargetId}");
    }
}
