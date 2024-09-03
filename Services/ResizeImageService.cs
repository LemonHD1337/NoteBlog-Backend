using NoteBlog.Interfaces;

namespace NoteBlog.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class ResizeImageService : IResizeImageService
{
    public async Task ResizeImage(string inputPath, string outputPath, int width, int height)
    {
            using (var image = await Image.LoadAsync(inputPath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions()
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(width, height), 
                }));
                
                await image.SaveAsync(outputPath);
            }
    }
    
}