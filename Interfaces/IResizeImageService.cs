namespace NoteBlog.Interfaces;

public interface IResizeImageService
{
    Task ResizeImage(string inputImagePath, string outputImagePath, int width, int height);
}