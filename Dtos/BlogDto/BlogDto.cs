using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.Dtos.BlogContentDtos;
using NoteBlog.Models;

namespace NoteBlog.Dtos.BlogDto;

public class BlogDto
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Subtitles { get; set; } = String.Empty;
    public List<BlogContentDto> Contents { get; set; } = new List<BlogContentDto>();
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public int NumberOfViews { get; set; }
    public AppUserBlogDto AppUser { get; set; }
    public Tag Tag { get; set; }

}