﻿using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagementSystem.UI.Models;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
