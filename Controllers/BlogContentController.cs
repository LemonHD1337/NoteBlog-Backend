using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.BlogContentDtos;
using NoteBlog.QueryObjects;
using NoteBlog.Interfaces;
using NoteBlog.mappers;


namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogContentController : ControllerBase
    {
        private readonly IBlogContentRepository _repo;
        private readonly IFileService _fileService;

        public BlogContentController(IBlogContentRepository repository, IFileService fileService)
        {
            _repo = repository;
            _fileService = fileService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogContents = await _repo.GetAllAsync();

            var blogContentsDto = blogContents.Select(bc => bc.FromBlogContentModelToBlogContentDto());

            return Ok(blogContentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var blogContent = await _repo.GetByIdAsync(id);

            if (blogContent == null) return NotFound();

            return Ok(blogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogContent([FromForm] CreateBlogContentDto createBlogContentDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                if (createBlogContentDto.PictureFile != null)
                {
                    var imageFileName = await _fileService.UploadFile(createBlogContentDto.PictureFile);
                    createBlogContentDto.Picture = imageFileName;
                }

                if (createBlogContentDto.VideoFile != null)
                {
                    var videoFileName = await _fileService.UploadFile(createBlogContentDto.VideoFile);
                    createBlogContentDto.Video = videoFileName;
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error");
            }
            
            var createdBlogContent = await _repo.CreateAsync(createBlogContentDto.FromCreateBlogContentDtoToBlogContentModel());

            return CreatedAtAction(nameof(GetById), new { id = createdBlogContent.Id }, createdBlogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBlogContent([FromRoute] int id,[FromForm] UpdateBlogContentDto updateBlogContentDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var pictureFile = updateBlogContentDto.PictureFile;
            var videoFile = updateBlogContentDto.VideoFile;

            try
            {
                if (pictureFile != null || videoFile != null)
                {
                    var existingBlogContent = await _repo.FirstOrDefultAsync(id);
                    
                    if (existingBlogContent == null) return NotFound();
                    
                    if (pictureFile != null)
                    {
                        if (existingBlogContent.ImageName != null)
                        {
                            await _fileService.DeleteFile(existingBlogContent.ImageName);
                            updateBlogContentDto.Picture = await _fileService.UploadFile(pictureFile);   
                        }
                        
                        updateBlogContentDto.Picture = await _fileService.UploadFile(pictureFile);   
                    }

                    if (videoFile != null)
                    {
                        if (existingBlogContent.VideoName != null)
                        {
                            await _fileService.DeleteFile(existingBlogContent.VideoName);
                            updateBlogContentDto.Video = await _fileService.UploadFile(videoFile);   
                        }
                        
                        updateBlogContentDto.Video = await _fileService.UploadFile(videoFile);   
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error");
            }
            
            var updatedBlogContent = await _repo.UpdateAsync(id, updateBlogContentDto.FromUpdateBlogContentDtoToBlogContentModel());

            if (updatedBlogContent == null) return NotFound();

            return Ok(updatedBlogContent.FromBlogContentModelToBlogContentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBlogContent([FromRoute] int id)
        {
            var deletedBlogContent = await _repo.DeleteAsync(id);

            if (deletedBlogContent == null) return NotFound();

            if (deletedBlogContent.VideoName != null)
            {
                await _fileService.DeleteFile(deletedBlogContent.VideoName);
            }else if (deletedBlogContent.ImageName != null)
            {
                await _fileService.DeleteFile(deletedBlogContent.ImageName);
            }

            return NoContent();
        }
    }
}
