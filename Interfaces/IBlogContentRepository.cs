using NoteBlog.Helpers;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface IBlogContentRepository
{
    Task<List<BlogContent>> GetAllAsync(PaginationQueryObject paginationQueryObject);
    Task<BlogContent?> GetByIdAsync(int id);
    Task<BlogContent> CreateAsync(BlogContent blogContent);
    Task<List<BlogContent>> CreateManyAsync(IEnumerable<BlogContent> blogContents);
    Task<BlogContent?> UpdateAsync(int id, BlogContent blogContent);
    Task<BlogContent?> DeleteAsync(int id);
}