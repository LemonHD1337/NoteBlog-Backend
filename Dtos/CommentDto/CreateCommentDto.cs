using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.CommentDto;

public class CreateCommentDto
{
    [Required]
    public string AppUserId { get; set; } = String.Empty;
    [Required]
    [StringLength(250)]
    public string Content { get; set; } = String.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;
    [Required]
    public int BlogId { get; set; }
}