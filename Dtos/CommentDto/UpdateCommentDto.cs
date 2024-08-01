using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.CommentDto;

public class UpdateCommentDto
{
    [Required]
    [StringLength(250)]
    public string Content { get; set; } = String.Empty;
}