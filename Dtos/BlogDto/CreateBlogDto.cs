using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.BlogDto;

public class CreateBlogDto
{
    [Required]
    [StringLength(60)]
    public string Title { get; set; } = String.Empty;
    [Required]
    public IFormFile ImageFile { get; set; }
    public string? ImageName { get; set; } = String.Empty;
    [Required]
    [StringLength(250)]
    public string Subtitles { get; set; } = String.Empty;
    [Required]
    public string AppUserId { get; set; } = String.Empty;
    [Required]
    public int TagId { get; set; }
}