using NoteBlog.Dtos.BlogContentDtos;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class BlogContentMapper
{
    public static BlogContentDto FromBlogContentModelToBlogContentDto(this BlogContent blogContent)
    {
        return new BlogContentDto()
        {
            Id = blogContent.Id,
            Title = blogContent.Title,
            Content = blogContent.Content,
            Picture = blogContent.ImageName,
            Video = blogContent.VideoName,
            BlogId = blogContent.BlogId,
            Layout =  blogContent.Layout
        };
    }

    public static BlogContent FromCreateBlogContentDtoToBlogContentModel(this CreateBlogContentDto createBlogContentDto)
    {
        return new BlogContent()
        {
            Layout = createBlogContentDto.Layout,
            Title = createBlogContentDto.Title,
            Content = createBlogContentDto.Content,
            ImageName = createBlogContentDto.Picture,
            VideoName = createBlogContentDto.Video,
            BlogId = createBlogContentDto.BlogId,
        };
    }

    public static BlogContent FromUpdateBlogContentDtoToBlogContentModel(this UpdateBlogContentDto updateBlogContentDto)
    {
        return new BlogContent()
        {
            Title = updateBlogContentDto.Title,
            Content = updateBlogContentDto.Content,
            ImageName = updateBlogContentDto.Picture,
            VideoName = updateBlogContentDto.Video,
            Layout = updateBlogContentDto.Layout,
        };
    }
}