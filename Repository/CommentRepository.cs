using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
using NoteBlog.QueryObjects;
using NoteBlog.Interfaces;
using NoteBlog.Models;

namespace NoteBlog.Repository;

public class CommentRepository: ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await Include().ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await Include().FirstOrDefaultAsync(c=>c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
    {
        var existingComment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id == id);

        if (existingComment == null)
            return existingComment;


        existingComment.Content = commentModel.Content;

        await _context.SaveChangesAsync();

        var updatedComment = await Include().FirstOrDefaultAsync(c => c.Id == id);

        return updatedComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment =  await Include().FirstOrDefaultAsync(c=>c.Id == id);

        if (comment == null) return comment;
        
         _context.Comments.Remove(comment);
         await _context.SaveChangesAsync();

         return comment;
    }

    public IQueryable<Comment> Include()
    {
        return _context.Comments.Include(c => c.AppUser);
    }

    public async Task<List<Comment>> GetByBlogIdAsync(int blogId)
    {
        return _context.Comments.Where(b=>b.BlogId == blogId).Include(c => c.AppUser).ToList();
    }
}