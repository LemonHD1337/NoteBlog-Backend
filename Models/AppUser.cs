using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NoteBlog.Models;

public class AppUser : IdentityUser
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = String.Empty;
    [Column(TypeName = "varchar(100)")]
    public string Surname { get; set; } = String.Empty;
    [Column(TypeName = "varchar(250)")]
    public string? Bio { get; set; } = String.Empty;
    public int CreatedBlogs { get; set; } = 0;
    public List<Blog> Blogs { get; set; } = new List<Blog>();
    public List<SocialMediaLink> Links { get; set; } = new List<SocialMediaLink>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
}