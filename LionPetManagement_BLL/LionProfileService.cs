using LionPetManagement_DAO;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_BLL;

public class LionProfileService
{
    private readonly LionProfileRepository _lionProfileRepository;
    public LionProfileService(LionProfileRepository lionProfileRepository)
    {
        _lionProfileRepository = lionProfileRepository;
    }
    public async Task<LionProfile?> GetProfileByIdAsync(int id)
    {
        return await _lionProfileRepository.GetByIdAsync(id);
    }
    
    public async Task<List<LionProfile>> GetAllProfilesAsync(int pageNumber = 1, int pageSize = 3)
    {
        return await _lionProfileRepository.GetAllAsync(pageNumber, pageSize);
    }
    public async Task<int> GetTotalProfileCountAsync()
    {
        return await _lionProfileRepository.GetCountAsync();
    }
}