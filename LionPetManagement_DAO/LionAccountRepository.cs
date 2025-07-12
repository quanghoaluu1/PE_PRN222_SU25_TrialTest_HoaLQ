using LionPetManagement_HoaLQ.Models;
using Microsoft.EntityFrameworkCore;

namespace LionPetManagement_DAO;

public class LionAccountRepository
{
    private readonly SU25LionDBContext _context;
    public LionAccountRepository(SU25LionDBContext context)
    {
        _context = context;
    }
    
    public async Task<LionAccount?> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.LionAccounts
            .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
    }
}