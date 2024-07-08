namespace BuildMentor.Models;

public class RegisterModel
{
    public string Login { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public IFormFile? UploadedAvatar { get; set; }
}