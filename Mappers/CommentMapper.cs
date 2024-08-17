using NoteBlog.Dtos.CommentDto;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class CommentMapper
{
    public static CommentDto FromCommentModelToCommentDto(this Comment commentModel)
    {
        return new CommentDto()
        {
            Content = commentModel.Content,
            CreateOn = commentModel.CreateOn,
            Id = commentModel.Id,
            UserName = commentModel.AppUser.UserName,
            Name = commentModel.AppUser.Name,
            Surname = commentModel.AppUser.Surname,
            ProfileImage = commentModel.AppUser.ProfileImage
        };
    }
    public static Comment FromCreateCommentDtoToCommentModel(this CreateCommentDto createCommentDto)
    {
        return new Comment()
        {
            AppUserId = createCommentDto.AppUserId,
            BlogId = createCommentDto.BlogId,
            CreateOn = createCommentDto.CreateOn,
            Content = createCommentDto.Content,
        };
    }

    public static Comment FromUpdateCommentDtoToCommentModel(this UpdateCommentDto updateCommentDto)
    {
        return new Comment()
        {
            Content = updateCommentDto.Content
        };
    }
}