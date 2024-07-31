using NoteBlog.Dtos.BlogContentDtos;

namespace NoteBlog.Dtos.BlogDto;

public class CreateBlogWithContentDto
{
    public string Title { get; set; } = String.Empty;
    public string Subtitles { get; set; } = String.Empty;
    public string AppUserId { get; set; } = String.Empty;
    public List<CreateBlogContentDto> Contents { get; set; } = new List<CreateBlogContentDto>();
    public int TagId { get; set; }
}