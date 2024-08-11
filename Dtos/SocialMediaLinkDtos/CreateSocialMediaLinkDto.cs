using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.SocialMediaLinkDto;

public class CreateSocialMediaLinkDto
{
    [Required]
    [StringLength(50)]
    public string SocialMediaName { get; set; } = String.Empty;
    [Required]
    [StringLength(300)]
    public string Link { get; set; } = String.Empty;
    [Required]
    public string AppUserId { get; set; } = String.Empty;
}