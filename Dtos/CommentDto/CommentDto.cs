
using NoteBlog.Models;

namespace NoteBlog.Dtos.CommentDto;

public class CommentDto
{
    public int Id { get; set; }
    public string AppUserId { get; set; } = string.Empty;
    public string Content { get; set; } = String.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public string UserName { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string? ProfileImage { get; set; } = String.Empty;
}