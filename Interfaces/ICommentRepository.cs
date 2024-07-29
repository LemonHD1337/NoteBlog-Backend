using NoteBlog.Helpers;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(PaginationQueryObject paginationQueryObject);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(int id, Comment commentModel);
    Task<Comment?> DeleteAsync(int id);
    
    IQueryable<Comment> Include();
}