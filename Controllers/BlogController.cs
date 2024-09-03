using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.BlogDto;
using NoteBlog.QueryObjects;
using NoteBlog.Interfaces;
using NoteBlog.mappers;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _repo;
        private readonly IFileService _fileService;

        public BlogController(IBlogRepository repository, IFileService fileService)
        {
            _repo = repository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BlogQueryObject blogQueryObject)
        {
            var blogs = await _repo.GetAllAsync(blogQueryObject);

           var blogDto = blogs.Blogs.Select(blog => blog.FromBlogModelToBlogDto());

            return Ok(new
            {
                totalPages = blogs.TotalPages,
                blogs = blogDto
            });
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
        public async Task<IActionResult> Create([FromForm] CreateBlogDto createBlogDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                createBlogDto.ImageName = await _fileService.UploadFile(createBlogDto.ImageFile);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error");
            }
            
            var createdBlog = await _repo.CreateAsync(createBlogDto.FromCreateBlogDtoToBlogModel());

            return CreatedAtAction(nameof(GetById), new { id = createdBlog.Id }, createdBlog);
        }
        

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateBlogDto updateBlogDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            if (updateBlogDto.ImageFile != null)
            {
                try
                {
                    var existingBlog = await _repo.FirstOrDefaultAsync(id);
                    if (existingBlog == null) return NotFound();


                    if (existingBlog.ImageName.Length != 0)
                    {
                        await _fileService.DeleteFile(existingBlog.ImageName);    
                    }
                    
                    updateBlogDto.ImageName = await _fileService.UploadFile(updateBlogDto.ImageFile);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Server error");
                }
                
            }
            
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

            foreach (var blogContent in deletedBlog.Contents)
            {
                try
                {
                    if (blogContent.ImageName != null)
                    {
                        await _fileService.DeleteFile(blogContent.ImageName);
                    }

                    if (blogContent.VideoName != null)
                    {
                        await _fileService.DeleteFile(blogContent.VideoName);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return Ok(deletedBlog);
        }
        
    }
}
