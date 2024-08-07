using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace NoteBlog.Dtos.AccountDto;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
    public bool IsRemember { get; set; } = false;
}