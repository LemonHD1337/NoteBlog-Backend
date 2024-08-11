using NoteBlog.Dtos.AppUserDtos;

namespace NoteBlog.Dtos.SocialMediaLinkDto;

public class SocialMediaLinksDto
{
    public int Id { get; set; }
    public string SocialMediaName { get; set; } = String.Empty;
    public string Link { get; set; } = String.Empty;
    public AppUserDto AppUser { get; set; } 
}