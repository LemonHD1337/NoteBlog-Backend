using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.AppUserDtos;

public class AppUserResetPassword
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Password { get; set; }
}