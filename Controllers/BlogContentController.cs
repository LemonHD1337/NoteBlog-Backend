using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.BlogContentDtos;
using NoteBlog.Helpers;
using NoteBlog.Interfaces;
using NoteBlog.mappers;
using NoteBlog.Models;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogContentController : ControllerBase
    {
        private readonly IBlogContentRepository _repo;

        public BlogContentController(IBlogContentRepository repository)
        {
            _repo = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQueryObject paginationQueryObject)
        {
            var blogContents = await _repo.GetAllAsync(paginationQueryObject);

            var blogContentsDtos = blogContents.Select(bc => bc.FromBlogContentModelToBlogContentDto());

            return Ok(blogContentsDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var blogContent = await _repo.GetByIdAsync(id);

            if (blogContent == null) return NotFound();

            return Ok(blogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogContent([FromBody] CreateBlogContentDto createBlogContentDto)
        {
            var createdBlogContent = await _repo.CreateAsync(createBlogContentDto.FromCreateBlogContentDtoToBlogContentModel());

            return CreatedAtAction(nameof(GetById), new { id = createdBlogContent.Id }, createdBlogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpPost("createManyBlogContents")]
        public async Task<IActionResult> CreateManyBlogContents([FromBody] List<CreateBlogContentDto> createBlogContentsDto)
        {
            var createBlogContentsModel = createBlogContentsDto.Select(b => b.FromCreateBlogContentDtoToBlogContentModel());

            var createdBlogContentsModel = await _repo.CreateManyAsync(createBlogContentsModel);

            var createdBlogContentsModelToDto = createdBlogContentsModel.Select(s => s.FromBlogContentModelToBlogContentDto());
            
            return CreatedAtAction(nameof(GetAll), createBlogContentsDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBlogContent([FromRoute] int id,[FromBody] UpdateBlogContentDto updateBlogContentDto)
        {
            var updatedBlogContent = await _repo.UpdateAsync(id, updateBlogContentDto.FromUpdateBlogContentDtoToBlogContentModel());

            if (updatedBlogContent == null) return NotFound();

            return Ok(updatedBlogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBlogContent([FromRoute] int id)
        {
            var deletedBlogContent = await _repo.DeleteAsync(id);

            if (deletedBlogContent == null) return NotFound();

            return NoContent();
        }
    }
}
