namespace NoteBlog.Helpers;

public class BlogQueryObject
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;
    public string? search { get; set; } = String.Empty;
    public bool IsDescsending { get; set; } = false;
}