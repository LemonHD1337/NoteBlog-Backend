using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
using NoteBlog.Dtos.BlogDto;
using NoteBlog.Helpers;
using NoteBlog.Interfaces;
using NoteBlog.Models;
using NuGet.Versioning;

namespace NoteBlog.Repository;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext _context;

    public BlogRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Blog>> GetAllAsync(PaginationQueryObject paginationQueryObject)
    {
        var skipNumber = (paginationQueryObject.PageNumber - 1) * paginationQueryObject.PageSize;
        return await Includes().Take(paginationQueryObject.PageSize).Skip(skipNumber).ToListAsync();
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await Includes().FirstOrDefaultAsync(b=>b.Id == id);
    }

    public async Task<Blog> CreateAsync(Blog blogModel)
    {
        await _context.Blogs.AddAsync(blogModel);
        await _context.SaveChangesAsync();
        return blogModel;
    }

    public async Task<Blog?> UpdateAsync(int id,UpdateBlogDto updateBlogDto)
    {
        var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

        if (existingBlog == null) return null;

        existingBlog.Title = updateBlogDto.Title;
        existingBlog.Subtitles = updateBlogDto.Subtitles;
        existingBlog.TagId = updateBlogDto.TagId;
        if(updateBlogDto.ImageName != null) existingBlog.ImageName = updateBlogDto.ImageName;

        await _context.SaveChangesAsync();
        
        return await Includes().FirstOrDefaultAsync(b=>b.Id == id);
    }

    public async Task<Blog?> DeleteAsync(int id)
    {
        var deleteBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

        if (deleteBlog == null) return null;

        var deletedContentWithBlogContents = await _context.Blogs.Include(b => b.Contents).FirstOrDefaultAsync(b => b.Id == id);

        _context.Blogs.Remove(deleteBlog);
        await _context.SaveChangesAsync();

        return deletedContentWithBlogContents;
    }

    public IQueryable<Blog> Includes()
    {
        return _context.Blogs
            .Include(b => b.Contents)
            .Include(b => b.AppUser)
            .Include(b=>b.Tag)
            .AsQueryable();
    }

    public async Task<Blog?> FirstOrDefaultAsync(int id)
    {
        return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
    }
}