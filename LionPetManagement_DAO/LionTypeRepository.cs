using LionPetManagement_DAO.Basic;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_DAO;

public class LionTypeRepository
{
    private readonly SU25LionDBContext _context;
    public LionTypeRepository(SU25LionDBContext context)
    {
        _context = context;
    }
    
    public new List<LionType> GetAll()
    {
        return _context.LionTypes.ToList();
    }
}