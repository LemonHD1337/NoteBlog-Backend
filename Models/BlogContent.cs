namespace NoteBlog.Models;

public class BlogContent
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public string? Picture { get; set; }
    public string? Video { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}