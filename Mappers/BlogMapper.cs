using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.Dtos.BlogContentDtos;
using NoteBlog.Dtos.BlogDto;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class BlogMapper
{
    public static BlogDto FromBlogModelToBlogDto(this Blog blogModel)
    {
        List<BlogContentDto> contentDtos = new List<BlogContentDto>();

        foreach (var c in blogModel.Contents)
        {
            var temp = new BlogContentDto()
            {
                Title = c.Title,
                Content = c.Content,
                Picture = c.ImageName,
                Video = c.VideoName,
                Id = c.Id,
                BlogId = c.BlogId
            };
            
            contentDtos.Add(temp);
        }
        
        return new BlogDto()
        {
            Id = blogModel.Id,
            Title = blogModel.Title,
            Subtitles = blogModel.Subtitles,
            CreateOn = blogModel.CreateOn,
            NumberOfViews = blogModel.NumberOfViews,
            AppUser = new AppUserDto()
            {
                Id = blogModel.AppUser.Id,
                Name = blogModel.AppUser.Name,
                Surname = blogModel.AppUser.Surname
            },
            Contents = contentDtos,
            Tag = blogModel.Tag,
        };
    }
    public static Blog FromCreateBlogDtoToBlogModel(this CreateBlogDto createBlogDto)
    {
        return new Blog()
        {
            Title = createBlogDto.Title,
            Subtitles = createBlogDto.Subtitles,
            AppUserId = createBlogDto.AppUserId,
            TagId = createBlogDto.TagId,
            ImageName = createBlogDto.ImageName
        };
    }

    public static Blog FromCreateBlogWithContentsDtoToBlogModel(this CreateBlogWithContentDto createBlogWithContentDto)
    {
        
        var blog =  new Blog()
        {
            TagId = createBlogWithContentDto.TagId,
            AppUserId = createBlogWithContentDto.AppUserId,
            Subtitles = createBlogWithContentDto.Subtitles,
            Title = createBlogWithContentDto.Title
        };
        
        var blogContentsModel = new List<BlogContent>();

        foreach (var createBlogContentsDto in createBlogWithContentDto.Contents)
        {
            var blogContent = new BlogContent()
            {
                Title = createBlogContentsDto.Title,
                Content = createBlogContentsDto.Content,
                ImageName = createBlogContentsDto.Picture,
                VideoName = createBlogContentsDto.Video,
                BlogId = blog.Id
            };
            
            blogContentsModel.Add(blogContent);
        }

        blog.Contents = blogContentsModel;
        return blog;
    }
}