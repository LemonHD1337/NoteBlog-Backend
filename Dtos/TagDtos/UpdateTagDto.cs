using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.TagDtos;

public class UpdateTagDto
{
    [Required]
    [StringLength(100)]
    public string TagName { get; set; } = String.Empty;
}