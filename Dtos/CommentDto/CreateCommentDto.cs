namespace NoteBlog.Dtos.CommentDto;

public class CreateCommentDto
{
    public string AppUserId { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public int BlogId { get; set; }
}