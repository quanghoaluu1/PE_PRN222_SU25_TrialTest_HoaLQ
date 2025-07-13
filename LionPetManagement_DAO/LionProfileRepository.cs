using LionPetManagement_DAO.Basic;
using LionPetManagement_HoaLQ.Models;
using Microsoft.EntityFrameworkCore;

namespace LionPetManagement_DAO;

public class LionProfileRepository: GenericRepository<LionProfile>
{
    private readonly SU25LionDBContext _context;
    public LionProfileRepository(SU25LionDBContext context) : base(context)
    {
        _context = context;
    }

    public new async Task<LionProfile?> GetByIdAsync(int id)
    {   
        return await _context.LionProfiles
            .Include(l => l.LionType)
            .FirstOrDefaultAsync(l => l.LionProfileId == id);
    }
    
    public async Task<List<LionProfile>> GetAllAsync(int pageNumber = 1, int pageSize = 3)
    {
        return await _context.LionProfiles
            .Include(l => l.LionType)
            .OrderByDescending(l => l.ModifiedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<LionProfile>> SearchAsync(double weight, string? name, int pageNumber = 1, int pageSize = 3)
    {
        var query = _context.LionProfiles
            .Include(l => l.LionType)
            .AsQueryable();
        if (weight > 0)
        {
            query = query.Where(l => l.Weight == weight);
        }
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(l => l.LionName.Contains(name));
        }
        return await query
            .OrderByDescending(l => l.ModifiedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.LionProfiles.CountAsync();
    }
    
}