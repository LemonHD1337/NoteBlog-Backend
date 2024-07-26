using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBlog.Models;

public class Blog
{
    public int Id { get; set; }
    [Column(TypeName = "char(250)")]
    public string Title { get; set; } = String.Empty;
    [Column(TypeName = "char(150)")]
    public string Subtitles { get; set; } = String.Empty;
    public List<BlogContent> Contents { get; set; } = new List<BlogContent>();
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public uint NumberOfViews { get; set; }
    public string AppUserId { get; set; } = String.Empty;
    public AppUser AppUser { get; set; }
    public int TagId { get; set; }
    public List<Comment>? Comments { get; set; } = new List<Comment>();
}