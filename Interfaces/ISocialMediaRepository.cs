using NoteBlog.Dtos.SocialMediaLinkDto;
using NoteBlog.Models;

namespace NoteBlog.Interfaces;

public interface ISocialMediaRepository
{
    Task<List<SocialMediaLink>> GetAllAsync();
    Task<SocialMediaLink?> GetByIdAsync(int id);
    Task<SocialMediaLink> CreateAsync(SocialMediaLink socialMediaLink);
    Task<SocialMediaLink?> UpdateAsync(int id, UpdateSocialMediaLinkDto updateSocialMediaLinkDto);
    Task<SocialMediaLink?> DeleteAsync(int id);
    IQueryable<SocialMediaLink> Include();
}