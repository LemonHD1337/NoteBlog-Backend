
namespace NoteBlog.Interfaces;

public interface IFileService
{
    public Task<string> UploadFile(IFormFile file);
    public Task<string> DeleteFile(string fileName);
    public string GenerateFileName(string path, IFormFile file);
    public string GetFileExtension(string fileName);
}