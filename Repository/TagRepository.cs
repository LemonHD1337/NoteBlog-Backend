using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
using NoteBlog.Interfaces;
using NoteBlog.Models;

namespace NoteBlog.Repository;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;

    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null) 
            return null;

        return tag;
    }

    public async Task<Tag> CreateAsync(Tag tagModel)
    {
        await _context.Tags.AddAsync(tagModel);
        await _context.SaveChangesAsync();
        return tagModel;
    }
    
    public async Task<Tag?> UpdateAsync(int id, Tag tagModel)
    {
        var existingTag = await _context.Tags.FindAsync(id);

        if (existingTag == null)
            return null;

        existingTag.TagName = tagModel.TagName;

        await _context.SaveChangesAsync();

        return existingTag;

    }

    public async Task<Tag?> DeleteAsync(int id)
    {
        var deleteTag = await _context.Tags.FindAsync(id);

        if (deleteTag == null)
            return null;

        _context.Tags.Remove(deleteTag);
        await _context.SaveChangesAsync();
        return deleteTag;
    }
}