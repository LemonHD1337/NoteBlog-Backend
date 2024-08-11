using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.TagDtos;
using NoteBlog.QueryObjects;
using NoteBlog.Interfaces;
using NoteBlog.mappers;


namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repo;

        public TagController(ITagRepository repository)
        {
            _repo = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _repo.GetAllAsync();
            
            return Ok(tags);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tag = await _repo.GetByIdAsync(id);

            if (tag == null) return NotFound();
            
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var createdTag = await _repo.CreateAsync(tagDto.FromCreateDtoToTagModel());

            return CreatedAtAction(nameof(GetById), new {id = createdTag.Id}, createdTag);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateTagDto tagDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var updatedTag = await _repo.UpdateAsync(id, tagDto.FromUpdateDtoToTagModel());

            if (updatedTag == null)
                return NotFound();
            
            return Ok(updatedTag);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedTag = await _repo.DeleteAsync(id);

            if (deletedTag == null)
                return NotFound();

            return NoContent();
        }
    }
}
