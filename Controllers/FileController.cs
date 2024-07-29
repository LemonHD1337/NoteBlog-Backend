using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _uploads;
        
        public FileController(IConfiguration config)
        {
            _uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            
            if (!Directory.Exists(_uploads)) Directory.CreateDirectory(_uploads);
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            
            string fileType = file.FileName.Split(".").Last();
            string fileName = $@"{DateTime.Now.Ticks}.{fileType}";
            string filePath = Path.Combine(_uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            return Ok(fileName);
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            var fullPath = Path.Combine(_uploads, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            try
            {
                await Task.Run(() =>System.IO.File.Delete(fullPath));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return NoContent();
        }
    }
}
