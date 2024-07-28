namespace NoteBlog.Dtos.BlogDto;

public class UpdateBlogDto
{
    public string Title { get; set; } = String.Empty;
    public string Subtitles { get; set; } = String.Empty;
    public int TagId { get; set; }
}