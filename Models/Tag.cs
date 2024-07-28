using System.ComponentModel.DataAnnotations.Schema;

namespace NoteBlog.Models;

public class Tag
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string TagName { get; set; } = String.Empty;
}