using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.SocialMediaLinkDtos;

public class UpdateSocialMediaLinkDto
{
    [Required]
    [StringLength(50)]
    public string SocialMediaName { get; set; } = String.Empty;
    [Required]
    [StringLength(300)]
    public string Link { get; set; } = String.Empty;
}