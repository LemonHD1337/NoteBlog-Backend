using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
using NoteBlog.Interfaces;
using NoteBlog.Models;

namespace NoteBlog.Repository;

public class BlogContentRepository: IBlogContentRepository
{
    private readonly ApplicationDbContext _context;

    public BlogContentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<BlogContent>> GetAllAsync()
    {
        return await _context.BlogContents.ToListAsync();
    }

    public async Task<BlogContent?> GetByIdAsync(int id)
    {
        var blogContent = await _context.BlogContents.FindAsync(id);
        if (blogContent == null) return null;

        return blogContent;
    }

    public async Task<BlogContent> CreateAsync(BlogContent blogContent)
    {
        await _context.BlogContents.AddAsync(blogContent);
        await _context.SaveChangesAsync();
        return blogContent;
    }

    public async Task<List<BlogContent>> CreateManyAsync(IEnumerable<BlogContent> blogContents)
    {
        await _context.BlogContents.AddRangeAsync(blogContents);
        await _context.SaveChangesAsync();
        return blogContents.ToList();
    }

    public async Task<BlogContent?> UpdateAsync(int id, BlogContent blogContent)
    {
        var existingContent = await _context.BlogContents.FirstOrDefaultAsync(c => c.Id == id);

        if (existingContent == null) return null;

        existingContent.Title = blogContent.Title;
        existingContent.Content = existingContent.Content;
        existingContent.Picture = blogContent.Picture;
        existingContent.Video = blogContent.Video;
        
        await _context.SaveChangesAsync();
        return existingContent;
    }

    public async Task<BlogContent?> DeleteAsync(int id)
    {
        var deletedContent = await _context.BlogContents.FirstOrDefaultAsync(c => c.Id == id);

        if (deletedContent == null) return null;

        _context.BlogContents.Remove(deletedContent);

        await _context.SaveChangesAsync();

        return deletedContent;
    }
}