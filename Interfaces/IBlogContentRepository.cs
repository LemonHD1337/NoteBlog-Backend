using NoteBlog.QueryObjects;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface IBlogContentRepository
{
    Task<List<BlogContent>> GetAllAsync();
    Task<BlogContent?> GetByIdAsync(int id);
    Task<BlogContent> CreateAsync(BlogContent blogContent);
    Task<List<BlogContent>> CreateManyAsync(IEnumerable<BlogContent> blogContents);
    Task<BlogContent?> UpdateAsync(int id, BlogContent blogContent);
    Task<BlogContent?> DeleteAsync(int id);
    Task<BlogContent?> FirstOrDefultAsync(int id);
}