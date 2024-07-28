using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.Dtos.SocialMediaLinkDtos;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class SocialMediaLinkMapper
{
    public static SocialMediaLinksDto FromSocialMediaLinkModelToSocialMediaLinksDto(this SocialMediaLink socialMediaLink)
    {
        return new SocialMediaLinksDto()
        {
            Id = socialMediaLink.Id,
            Link = socialMediaLink.Link,
            SocialMediaName = socialMediaLink.SocialMediaName,
            AppUser = new AppUserDto()
            {
                Id = socialMediaLink.AppUser.Id,
                Name = socialMediaLink.AppUser.Name,
                Surname = socialMediaLink.AppUser.Surname
            }
        };
    }

    public static SocialMediaLink FromCreateSocialMediaLinkDtoToSocialMediaLinkModel(this CreateSocialMediaLinkDto createSocialMediaLinkDto)
    {
        return new SocialMediaLink()
        {
            Link = createSocialMediaLinkDto.Link,
            AppUserId = createSocialMediaLinkDto.AppUserId,
            SocialMediaName = createSocialMediaLinkDto.SocialMediaName
        };
    }
}