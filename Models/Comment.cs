using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBlog.Models;

public class Comment
{
    public int Id { get; set; }
    public string AppUserId { get; set; } = String.Empty;
    public AppUser AppUser { get; set; }
    [Column(TypeName = "Text")]
    public string Content { get; set; } = String.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
}