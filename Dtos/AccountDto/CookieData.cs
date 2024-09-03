namespace NoteBlog.Dtos.AccountDto;

public class CookieData
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string Nickname { get; set; } = String.Empty;
    public IList<String> Role { get; set; } = new List<String>();
    public string ProfileImageName { get; set; } = String.Empty;
} 
