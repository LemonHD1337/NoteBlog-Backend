using NoteBlog.Dtos.BlogDto;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface IBlogRepository
{
    Task<List<Blog>> GetAllAsync();
    Task<Blog?> GetByIdAsync(int id);
    Task<Blog> CreateAsync(Blog blogModel);
    Task<Blog?> UpdateAsync(int id,UpdateBlogDto updateBlogDto);
    Task<Blog?> DeleteAsync(int id);
    IQueryable<Blog> Includes();

}