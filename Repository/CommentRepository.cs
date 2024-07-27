using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
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
        var comments = await JoinAppUser().ToListAsync();
        
        return comments;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await JoinAppUser().FirstOrDefaultAsync(c=>c.Id == id);
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

        var updatedComment = await JoinAppUser().FirstOrDefaultAsync(c => c.Id == id);

        return updatedComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment =  await JoinAppUser().FirstOrDefaultAsync(c=>c.Id == id);

        if (comment == null) return comment;
        
         _context.Comments.Remove(comment);
         await _context.SaveChangesAsync();

         return comment;
    }

    public IQueryable<Comment> JoinAppUser()
    {
        return _context.Comments.Join(_context.Users,
            comment => comment.AppUserId,
            user => user.Id,
            (comment, user) => new Comment()
            {
                Id = comment.Id,
                BlogId = comment.BlogId,
                Content = comment.Content,
                AppUser = user,
                CreateOn = comment.CreateOn,
            }
        );
    }
}