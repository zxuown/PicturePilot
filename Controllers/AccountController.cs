using BuildMentor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Data.Entities;
using System.Data;
using PicturePilot.Business.Services;
using PicturePilot.Data;

namespace PicturePilot.Controllers;

public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ImageService imageService, IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager, PicturesDbContext context) : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
    private readonly PicturesDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;
    private readonly ImageService _imageService = imageService;
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        model.Login = model.Login?.Trim();
        if (string.IsNullOrEmpty(model.Login))
        {
            return BadRequest(new { Error = "Login is required." });
        }
        if (string.IsNullOrEmpty(model.Name))
        {
            return BadRequest(new { Error = "Name is required." });
        }
        if (string.IsNullOrEmpty(model.Email))
        {
            return BadRequest(new { Error = "Email is required." });
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest(new { Error = "Password is required." });
        }
        if (string.IsNullOrEmpty(model.ConfirmPassword))
        {
            return BadRequest(new { Error = "Confirm Password is required." });
        }
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new { Error = "Passwords do not match." });
        }
        if (model.Password.Length < 8)
        {
            return BadRequest(new { Error = "Password must be at least 8 characters long." });
        }
        if (model.Password.Any(char.IsLetter) == false)
        {
            return BadRequest(new { Error = "Password must contain at least one letter." });
        }
        if (model.Password.Any(char.IsDigit) == false)
        {
            return BadRequest(new { Error = "Password must contain at least one digit." });
        }
        var existingLogin = await _userManager.FindByNameAsync(model.Login);
        if (existingLogin != null)
        {
            return BadRequest(new { Error = "The login is already in use." });
        }

        var existingEmail = await _userManager.FindByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            return BadRequest(new { Error = "The email is already in use." });
        }
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new { Error = "Passwords do not match." });
        }

        var user = new User
        {
            Name = model.Name,
            Login = model.Login,
            UserName = model.Login,
            Email = model.Email,
            CreatedAt = DateTime.Now.Date
        };


        if (model.UploadedAvatar != null)
        {
            var avatar = await _imageService.Upload(model.UploadedAvatar);
            user.AvatarUrl = avatar;
        }
        else
        {
            user.AvatarUrl = null;
        }

        var result = await _userManager.CreateAsync(user, model.Password);

        try
        {
            await _userManager.AddToRoleAsync(user, "USER");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


        if (result.Succeeded)
        {
            return Ok(new { Message = "Success!", Role = (await _userManager.GetRolesAsync(user)).First() });
        }


        return BadRequest(new { Error = string.Join(",", result.Errors.Select(e => e.Description).ToList()) });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {

        model.LoginOrEmail = model.LoginOrEmail?.Trim();
        if (string.IsNullOrEmpty(model.LoginOrEmail))
        {
            return BadRequest(new { Message = "", Error = "No Such Login Exists" });
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest(new { Message = "", Error = "Wrong Password!" });
        }

        var existingEmail = await _userManager.FindByEmailAsync(model.LoginOrEmail);
        var existingLogin = await _userManager.FindByNameAsync(model.LoginOrEmail);
        if (existingLogin == null && existingEmail == null)
        {
            return BadRequest(new { Message = "", Error = "No Such Login Exists" });
        }
        if (existingEmail != null) { model.LoginOrEmail = existingEmail.UserName; }

        var result = await _signInManager.PasswordSignInAsync(model.LoginOrEmail, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return BadRequest(new { Message = "", Error = "Wrong Password!" });
        }
        var username = existingLogin ?? existingEmail;

        return Ok(new { Message = "Success!", admin = await _userManager.IsInRoleAsync(username, "ADMIN") });
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [HttpGet("/Account/AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    public async void MakeAdmin(int id)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "ADMIN");
            await _userManager.RemoveFromRoleAsync(user, "USER");
        }
    }
}
