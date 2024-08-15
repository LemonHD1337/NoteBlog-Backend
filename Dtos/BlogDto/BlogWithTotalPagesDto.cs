using NoteBlog.Models;

namespace NoteBlog.Dtos.BlogDto;

public class BlogWithTotalPagesDto
{
    public int TotalPages { get; set; } = 1;
    public List<Blog> Blogs { get; set; }
}