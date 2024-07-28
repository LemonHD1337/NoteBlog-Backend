using NoteBlog.Dtos.TagDtos;
using NoteBlog.Models;

namespace NoteBlog.mappers;

public static class TagMapper
{
    public static Tag FromCreateDtoToTagModel(this CreateTagDto data)
    {
        return new Tag()
        {
            TagName = data.TagName
        };
    }
    
    public static Tag FromUpdateDtoToTagModel(this UpdateTagDto data)
    {
        return new Tag()
        {
            TagName = data.TagName
        };
    }
}


