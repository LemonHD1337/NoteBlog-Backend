using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBlog.Models;

public class Blog
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string Title { get; set; } = String.Empty;
    [Column(TypeName = "varchar(250)")]
    public string Subtitles { get; set; } = String.Empty;
    public List<BlogContent> Contents { get; set; } = new List<BlogContent>();
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public int NumberOfViews { get; set; } = 0;
    public string AppUserId { get; set; } = String.Empty;
    public AppUser AppUser { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; } 
    public List<Comment>? Comments { get; set; } = new List<Comment>();
}