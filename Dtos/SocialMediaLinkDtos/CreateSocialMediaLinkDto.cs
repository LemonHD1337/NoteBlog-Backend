namespace NoteBlog.Dtos.SocialMediaLinkDtos;

public class CreateSocialMediaLinkDto
{
    public string SocialMediaName { get; set; } = String.Empty;
    public string Link { get; set; } = String.Empty;
    public string AppUserId { get; set; } = String.Empty;
}