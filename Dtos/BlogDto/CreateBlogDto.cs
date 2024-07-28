namespace NoteBlog.Dtos.BlogDto;

public class CreateBlogDto
{
    public string Title { get; set; } = String.Empty;
    public string Subtitles { get; set; } = String.Empty;
    public string AppUserId { get; set; } = String.Empty;
    public int TagId { get; set; }
}