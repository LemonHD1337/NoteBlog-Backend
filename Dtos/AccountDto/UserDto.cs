using NoteBlog.Dtos.SocialMediaLinkDto;

namespace NoteBlog.Dtos.AccountDto;


public class UserDto
{
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string? ProfileImage { get; set; } = String.Empty;
    public string? Bio { get; set; } = String.Empty;
    public List<SocialMediaLinkWithoutAppUser> Links { get; set; } = new List<SocialMediaLinkWithoutAppUser>();
}