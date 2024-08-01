using System.ComponentModel.DataAnnotations;

namespace NoteBlog.Dtos.TagDtos;

public class CreateTagDto
{
    [Required]
    [StringLength(100)]
    public string TagName { get; set; } = String.Empty;
}