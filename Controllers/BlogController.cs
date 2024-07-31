using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.BlogDto;
using NoteBlog.Helpers;
using NoteBlog.Interfaces;
using NoteBlog.mappers;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _repo;

        public BlogController(IBlogRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQueryObject paginationQueryObject)
        {
            var blogs = await _repo.GetAllAsync(paginationQueryObject);

            var blogsDto = blogs.Select(blog => blog.FromBlogModelToBlogDto());

            return Ok(blogsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var blog = await _repo.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            return Ok(blog.FromBlogModelToBlogDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogDto createBlogDto)
        {
            var createdBlog = await _repo.CreateAsync(createBlogDto.FromCreateBlogDtoToBlogModel());

            return CreatedAtAction(nameof(GetById), new { id = createdBlog.Id }, createdBlog);
        }

        [HttpPost("CreateWithContents")]
        public async Task<IActionResult> CreateWithContents([FromBody] CreateBlogWithContentDto createBlogWithContentDto)
        {
            var createdBlog = await _repo.CreateAsync(createBlogWithContentDto.FromCreateBlogWithContentsDtoToBlogModel());

            return CreatedAtAction(nameof(GetById), new { id = createdBlog.Id }, createdBlog);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBlogDto updateBlogDto)
        {
            var updatedBlog = await _repo.UpdateAsync(id, updateBlogDto);

            if (updatedBlog == null)
                return NotFound();

            return Ok(updatedBlog.FromBlogModelToBlogDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedBlog = await _repo.DeleteAsync(id);

            if (deletedBlog == null)
                return NotFound();

            return NoContent();
        }
        
    }
}
