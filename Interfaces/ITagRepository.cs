using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync();
    Task<Tag?> GetByIdAsync(int id);
    Task<Tag> CreateAsync(Tag tagModel);
    Task<Tag?> UpdateAsync(int id, Tag tagModel);
    Task<Tag?> DeleteAsync(int id);
}