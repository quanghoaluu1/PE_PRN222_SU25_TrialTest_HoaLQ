using LionPetManagement_HoaLQ.Models;
using Microsoft.EntityFrameworkCore;

namespace LionPetManagement_DAO;

public class LionProfileRepository
{
    private readonly SU25LionDBContext _context;
    public LionProfileRepository(SU25LionDBContext context)
    {
        _context = context;
    }

    public async Task<LionProfile?> GetByIdAsync(int id)
    {   
        return await _context.LionProfiles
            .Include(l => l.LionType)
            .FirstOrDefaultAsync(l => l.LionProfileId == id);
    }
    
    public async Task<List<LionProfile>> GetAllAsync(int pageNumber = 1, int pageSize = 3)
    {
        return await _context.LionProfiles
            .Include(l => l.LionType)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.LionProfiles.CountAsync();
    }
    
}