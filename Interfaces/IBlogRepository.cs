using NoteBlog.Dtos.BlogDto;
using NoteBlog.QueryObjects;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface IBlogRepository
{
    Task<BlogWithTotalPagesDto> GetAllAsync(BlogQueryObject query);
    Task<Blog?> GetByIdAsync(int id);
    Task<Blog> CreateAsync(Blog blogModel);
    Task<Blog?> UpdateAsync(int id,UpdateBlogDto updateBlogDto);
    Task<Blog?> DeleteAsync(int id);
    IQueryable<Blog> Includes();
    Task<Blog?> FirstOrDefaultAsync(int id);

}