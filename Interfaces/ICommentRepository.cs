using NoteBlog.QueryObjects;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(int id, Comment commentModel);
    Task<Comment?> DeleteAsync(int id);
    IQueryable<Comment> Include();
    Task<List<Comment>> GetByBlogIdAsync(int blogId);
}