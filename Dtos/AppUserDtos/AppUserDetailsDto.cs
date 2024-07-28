using NoteBlog.Dtos.SocialMediaLinkDtos;
using NoteBlog.Models;

namespace NoteBlog.Dtos.AppUserDtos;

public class AppUserDetailsDto
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string? Bio { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public List<SocialMediaLinkWithoutAppUser> Links { get; set; } = new List<SocialMediaLinkWithoutAppUser>();
}