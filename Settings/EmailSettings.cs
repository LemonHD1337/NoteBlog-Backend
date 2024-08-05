namespace NoteBlog.Settings;

public class EmailSettings
{
    public string MailServer { get; set; } = String.Empty;
    public int MailPort { get; set; }
    public bool UseSsl { get; set; }
    public string SenderName { get; set; }= String.Empty;
    public string FromEmail { get; set; }= String.Empty;
    public string Password { get; set; }= String.Empty;
}