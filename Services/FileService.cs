using NoteBlog.Interfaces;

namespace NoteBlog.Services;

public class FileService: IFileService
{
    private readonly string _uploads;
    private readonly IResizeImageService _resizeImageService;

    public FileService(IResizeImageService resizeImageService, string folderName = "Uploads")
    {
        _uploads = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        _resizeImageService = resizeImageService;
            
        if (!Directory.Exists(_uploads)) Directory.CreateDirectory(_uploads);
    }
    
    public async Task<string> UploadFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file uploaded");
            }
            
            string fileName = GenerateFileName(_uploads, file);
        
            string filePath = Path.Combine(_uploads, fileName);
            
            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (file.FileName.Equals("PictureFile"))
            {
                await _resizeImageService.ResizeImage(filePath, filePath, 700, 500);    
            }
            
            return fileName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> DeleteFile(string fileName)
    {
        try
        {
            var fullPath = Path.Combine(_uploads, fileName);

            if (!File.Exists(fullPath))
            {
                throw new Exception("File doesn't exists");
            }
            
            await Task.Run(() =>
            {
                File.Delete(fullPath);
            });

            return fileName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public string GenerateFileName(string path, IFormFile file)
    {
        string fileType = file.FileName.Split(".").Last();
        string fileName = $@"{DateTime.Now.Ticks}.{fileType}";
        return fileName;
    }

    public string GetFileExtension(string fileName)
    {
        return fileName.Split(".").Last();
    }
}