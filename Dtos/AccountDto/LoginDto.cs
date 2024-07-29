using System.Runtime.InteropServices.JavaScript;

namespace NoteBlog.Dtos.AccountDto;

public class LoginDto
{
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}