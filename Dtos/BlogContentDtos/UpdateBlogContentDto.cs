using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.BlogContentDtos;

public class UpdateBlogContentDto
{
    [StringLength(60)]
    public string? Title { get; set; } = String.Empty;
    [Required]
    public string Content { get; set; } = String.Empty;
    public int Layout { get; set; }
    public IFormFile? PictureFile { get; set; }
    public IFormFile? VideoFile { get; set; }
    public string? Picture { get; set; }
    public string? Video { get; set; }
}