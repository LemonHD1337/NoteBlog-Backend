using Microsoft.Build.Framework;

namespace NoteBlog.Dtos.AccountDto;

public class ForgotPasswordDto
{
    [Required]
    public string Email { get; set; } = String.Empty;
}