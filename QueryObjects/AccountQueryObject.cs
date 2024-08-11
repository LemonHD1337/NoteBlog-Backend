namespace NoteBlog.QueryObjects;

public class AccountQueryObject
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;
    public bool IsDescending { get; set; } = false;
    public string? SortBy { get; set; } = String.Empty;
}