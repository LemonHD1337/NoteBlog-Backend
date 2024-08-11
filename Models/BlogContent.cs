using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace NoteBlog.Models;

public class BlogContent
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string? Title { get; set; } = String.Empty;
    [Column(TypeName = "Text")]
    public string Content { get; set; } = String.Empty;
    [Column(TypeName = "varchar(50)")]
    public string? ImageName { get; set; } = String.Empty;
    [Column(TypeName = "varchar(50)")]
    public string? VideoName { get; set; } = String.Empty;
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}