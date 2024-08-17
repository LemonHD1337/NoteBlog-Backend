using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.CommentDto;
using NoteBlog.Interfaces;
using NoteBlog.mappers;


namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;

        public CommentController(ICommentRepository repository)
        {
            _repo = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commentsModel = await _repo.GetAllAsync();
            
            var commentsDto = commentsModel.Select(c => c.FromCommentModelToCommentDto());
            
            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _repo.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.FromCommentModelToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var createdComment = await _repo.CreateAsync(createCommentDto.FromCreateCommentDtoToCommentModel());
            
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var existingComment = await _repo.UpdateAsync(id, updateCommentDto.FromUpdateCommentDtoToCommentModel());

            if (existingComment == null) return NotFound();

            return Ok(existingComment.FromCommentModelToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedComment = await _repo.DeleteAsync(id);
            
            if (deletedComment == null) return NotFound();
            
            return NoContent();
        }

        [HttpGet("blog/{id:int}")]
        public async Task<IActionResult> GetByBlogId([FromRoute] int id)
        {
            var comments = await _repo.GetByBlogIdAsync(id);
            var commentsDto = comments.Select(c => c.FromCommentModelToCommentDto());
            return Ok(commentsDto);
        }
    }
}
