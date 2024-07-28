using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.Dtos.SocialMediaLinkDtos;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class AccountMapper
{
    public static AppUserDetailsDto FromAppUserToAppUserDetailsDto(this AppUser appUser)
    {
        var links = new List<SocialMediaLinkWithoutAppUser>();

        foreach (var link in appUser.Links)
        {
            var newLink = new SocialMediaLinkWithoutAppUser()
            {
                Id = link.Id,
                Link = link.Link,
                SocialMediaName = link.SocialMediaName
            };
            
            links.Add(newLink);
        }
        
        return new AppUserDetailsDto()
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            Email = appUser.Email,
            Name = appUser.Name,
            Bio = appUser.Bio,
            Links = links,
            Surname = appUser.Surname
        };
    }
}