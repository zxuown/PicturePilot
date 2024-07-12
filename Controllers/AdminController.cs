using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class AdminController : Controller
{
    private readonly ReportRepository _reportRepository;

    private readonly ImageRepository _imageRepository;

    private readonly UserRepository _userRepository;
    public AdminController(ReportRepository reportRepository, ImageRepository imageRepository, UserRepository userRepository, PicturesDbContext context)
    {
        _reportRepository = reportRepository;
        _imageRepository = imageRepository;
        _userRepository = userRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/Admin/Reports")]
    public async Task<IActionResult> Reports()
    {
        return View(await _reportRepository.GetAllAsync());
    }

    [HttpPost("/Admin/Reports/Block/{id}")]
    public async Task<IActionResult> Block(int id)
    {
        Report report = await _reportRepository.GetByIdAsync(id);
        if ((int)report.ReportType == 0)
        {
            User user = await _userRepository.GetByIdAsync(report.TargetId);
            user.IsBLocked = true;
            await _userRepository.UpdateAsync(user);
        }
        else
        {
            Image image = await _imageRepository.GetByIdAsync(report.TargetId);
            image.IsBLocked = true;
            await _imageRepository.UpdateAsync(image);
        }
        await _reportRepository.DeleteAsync(id);
        return Redirect("/Admin/Reports");
    }

    [HttpPost("/Admin/Reports/Innocent/{id}")]
    public async Task<IActionResult> Innocent(int id)
    {
        await _reportRepository.DeleteAsync(id);
        return Redirect("/Admin/Reports");
    }

    [HttpGet("/Admin/BlackListImages")]
    public async Task<IActionResult> BlackListImagesAsync()
    {
        return View(await _imageRepository.GetAllBlockedAsync());
    }

    [HttpGet("/Admin/BlackListUsers")]
    public async Task<IActionResult> BlackListUsers()
    {
        return View(await _userRepository.GetAllBlockedAsync());
    }

    [HttpPost("/Admin/User/Unblock/{id}")]
    public async Task<IActionResult> UnblockUser(int id)
    {
        User user = await _userRepository.GetByIdAsync(id);
        user.IsBLocked = false;
        await _userRepository.UpdateAsync(user);
        return Redirect("/Admin/BlackListUsers");
    }

    [HttpPost("/Admin/Image/Unblock/{id}")]
    public async Task<IActionResult> UnblockImage(int id)
    {
        Image image = await _imageRepository.GetByIdAsync(id);
        image.IsBLocked = false;
        await _imageRepository.UpdateAsync(image);
        return Redirect("/Admin/BlackListImages");
    }
}
