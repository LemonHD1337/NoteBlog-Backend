using NoteBlog.Dtos.AccountDto;
using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.Dtos.SocialMediaLinkDto;
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

    public static UserDto FromAppUserToUserDto(this AppUser appUser)
    {
        var list = new List<SocialMediaLinkWithoutAppUser>();

        foreach (var link in appUser.Links)
        {
            list.Add(new SocialMediaLinkWithoutAppUser()
            {
                Id = link.Id,
                SocialMediaName = link.SocialMediaName,
                Link = link.Link,
            });
        }

        return new UserDto()
        {
            Surname = appUser.Surname,
            Name = appUser.Name,
            Bio = appUser.Bio,
            Links = list,
            ProfileImage = appUser.ProfileImage,
        };
    }
}