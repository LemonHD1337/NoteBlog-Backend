using Microsoft.EntityFrameworkCore;
using NoteBlog.Data;
using NoteBlog.Dtos.SocialMediaLinkDto;
using NoteBlog.Interfaces;
using NoteBlog.Models;

namespace NoteBlog.Repository;

public class SocialMediaLinkRepository : ISocialMediaRepository
{
    private readonly ApplicationDbContext _context;

    public SocialMediaLinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<SocialMediaLink>> GetAllAsync()
    {
        return await Include().ToListAsync();
    }

    public async Task<SocialMediaLink?> GetByIdAsync(int id)
    {
        var socialMediaLink = await Include().FirstOrDefaultAsync(s => s.Id == id);

        if (socialMediaLink == null) return null;

        return socialMediaLink;
    }

    public async Task<SocialMediaLink> CreateAsync(SocialMediaLink socialMediaLink)
    {
        await _context.SocialMediaLinks.AddAsync(socialMediaLink);
        await _context.SaveChangesAsync();
        return socialMediaLink;
    }

    public async Task<SocialMediaLink?> UpdateAsync(int id, UpdateSocialMediaLinkDto updateSocialMediaLinkDto)
    {
        var existingSml = await _context.SocialMediaLinks.FirstOrDefaultAsync(s => s.Id == id);

        if (existingSml == null) return null;

        existingSml.Link = updateSocialMediaLinkDto.Link;
        existingSml.SocialMediaName = updateSocialMediaLinkDto.SocialMediaName;

        await _context.SaveChangesAsync();

        var updatedSml = await Include().FirstOrDefaultAsync(s => s.Id == id);

        return updatedSml;
    }

    public async Task<SocialMediaLink?> DeleteAsync(int id)
    {
        var deleteSml = await _context.SocialMediaLinks.FirstOrDefaultAsync(s => s.Id == id);

        if (deleteSml == null) return null;

        _context.SocialMediaLinks.Remove(deleteSml);
        await _context.SaveChangesAsync();

        return deleteSml;
    }

    public IQueryable<SocialMediaLink> Include()
    {
        return _context.SocialMediaLinks.Include(s => s.AppUser);
    }
}