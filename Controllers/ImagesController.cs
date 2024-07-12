using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PicturePilot.Business.Models;
using PicturePilot.Business.Services;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;

namespace PicturePilot.Controllers;

public class ImagesController(ImageService imageService, ImageRepository imageRepository, UserRepository userRepository, UserManager<User> userManager) : Controller
{
    private readonly ImageService _imageService = imageService;
    private readonly ImageRepository _imageRepository = imageRepository;
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<User> _userManager = userManager;

    [Authorize]
    [HttpGet("/Images/Add")]
    public IActionResult AddImage()
    {
        return View("/Views/Images/AddImage.cshtml");
    }

    [Authorize]
    [HttpPost("/Images/Add/Submit")]
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
                Tags = new HashSet<Tag>()
            };
            foreach (var tag in analysis.Tags)
            {
                image.Tags.Add(new Tag { Title = tag.Name });
            }
            var id = await _imageRepository.AddAsync(image);
            return Redirect($"/View/Image/{id}");
        }

        return View(model);
    }

    [Authorize]
    [HttpGet("/Images/{id}/Favorite")]
    public async Task<IActionResult> Favorite(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var result = _userRepository.SwitchFavorite(user.Id, id);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("/Images/{id}/Comment")]
    public async Task<IActionResult> Comment(int id, [FromBody] CommentModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        var comment = new Comment
        {
            ImageId = id,
            UserId = user.Id,
            Text =  model.Text,
            CreatedAt = DateTime.Now
        };
        try
        {
            await _imageRepository.CreateCommentAsync(comment);
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
        return Ok(new { Message = "Success" });
    }
}
