using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteBlog.Models;

namespace NoteBlog.Data;

public class ApplicationDbContext: IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<BlogContent> BlogContents { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.AppUser)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

 
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin", NormalizedName = "ADMIN"
            },
            
            new IdentityRole
            {
                Name = "User", NormalizedName = "USER"
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}