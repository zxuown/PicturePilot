﻿namespace PicturePilot.Business.Models;

public class LoginModel
{
    public string LoginOrEmail { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; } = true;
}