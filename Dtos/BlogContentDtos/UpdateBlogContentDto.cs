namespace NoteBlog.Dtos.BlogContentDtos;

public class UpdateBlogContentDto
{
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public string? Picture { get; set; }
    public string? Video { get; set; }
}