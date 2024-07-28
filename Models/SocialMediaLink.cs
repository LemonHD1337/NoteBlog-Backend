using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBlog.Models;

public class SocialMediaLink
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string SocialMediaName { get; set; } = String.Empty;
    [Column(TypeName = "varchar(500)")]
    public string Link { get; set; } = String.Empty;
    public string AppUserId { get; set; } = String.Empty;
    public AppUser AppUser { get; set; } 
}