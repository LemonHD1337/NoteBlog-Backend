using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBlog.Dtos.SocialMediaLinkDtos;
using NoteBlog.Interfaces;
using NoteBlog.mappers;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaLinkController : ControllerBase
    {
        private readonly ISocialMediaRepository _repo;

        public SocialMediaLinkController(ISocialMediaRepository repository)
        {
            _repo = repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var socialMediaLinks = await _repo.GetAllAsync();

            var socialMediaLinksDto = socialMediaLinks.Select(s => s.FromSocialMediaLinkModelToSocialMediaLinksDto());
            
            return Ok(socialMediaLinksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var socialMediaLink = await _repo.GetByIdAsync(id);

            if (socialMediaLink == null) return NotFound();

            return Ok(socialMediaLink.FromSocialMediaLinkModelToSocialMediaLinksDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialMediaLinkDto createSocialMediaLinkDto)
        {
            var createdSocialMediaLink = await _repo.CreateAsync(createSocialMediaLinkDto.FromCreateSocialMediaLinkDtoToSocialMediaLinkModel());

            return CreatedAtAction(nameof(GetById), 
                new { id = createdSocialMediaLink.Id },
                createdSocialMediaLink);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSocialMediaLinkDto updateSocialMediaLinkDto)
        {
            var updatedSocialMediaLink = await _repo.UpdateAsync(id, updateSocialMediaLinkDto);

            if (updatedSocialMediaLink == null) return NotFound();

            return Ok(updatedSocialMediaLink.FromSocialMediaLinkModelToSocialMediaLinksDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedSocialMediaLink = await _repo.DeleteAsync(id);

            if (deletedSocialMediaLink == null) return NotFound();

            return NoContent();
        }
    }
}
