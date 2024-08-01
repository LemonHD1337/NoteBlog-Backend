using NoteBlog.Models;

namespace NoteBlog.Dtos.BlogContentDtos;

public class BlogContentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public string? Picture { get; set; }
    public string? Video { get; set; }
    public int BlogId { get; set; }
}