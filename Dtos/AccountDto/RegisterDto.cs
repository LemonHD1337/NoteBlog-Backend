using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.AccountDto;

public class RegisterDto
{
    [Required]
    [StringLength(35)]
    public string Name { get; set; } = String.Empty;
    [Required]
    [StringLength(70)]
    public string Surname { get; set; } = String.Empty;
    [Required]
    [StringLength(50)]
    [DisplayName("User name")]
    public string Username { get; set; } = String.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
}