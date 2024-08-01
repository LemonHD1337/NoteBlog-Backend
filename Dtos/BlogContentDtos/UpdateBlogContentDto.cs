namespace NoteBlog.Dtos.BlogContentDtos;

public class UpdateBlogContentDto
{
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public IFormFile? PictureFile { get; set; }
    public IFormFile? VideoFile { get; set; }
    public string? Picture { get; set; }
    public string? Video { get; set; }
}