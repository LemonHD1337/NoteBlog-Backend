namespace NoteBlog.QueryObjects;

public class BlogQueryObject
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;
    public bool IsDescending { get; set; } = false;
    public string? UserName { get; set; } = String.Empty;
    public string? Name { get; set; } = String.Empty;
    public string? Surname { get; set; } = String.Empty;
    public string? Title { get; set; } = String.Empty;
    public string? Tag { get; set; } = String.Empty;
    public string? UserId { get; set; } = String.Empty;
    public string? SortBy { get; set; } = String.Empty;
}