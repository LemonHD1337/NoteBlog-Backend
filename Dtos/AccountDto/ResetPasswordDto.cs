using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.AccountDto;

/**
 * Token and ID props will be automatically retrieved in the frontend from the generated link's query parameters.
 */

public class ResetPasswordDto
{
    [Required]
    public string Password { get; set; }= String.Empty;
    [Required]
    public string Token { get; set; } = String.Empty;
    [Required]
    public string Id { get; set; } = String.Empty;
}