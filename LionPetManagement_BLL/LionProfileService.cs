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
    public async Task<int> CreateProfileAsync(LionProfile lionProfile)
    {
        return await _lionProfileRepository.CreateAsync(lionProfile);
    }
    public async Task<int> UpdateProfileAsync(LionProfile lionProfile)
    {
        return await _lionProfileRepository.UpdateAsync(lionProfile);
    }
    public async Task<bool> DeleteProfileAsync(int lionProfileId)
    {
        var lionProfile = await _lionProfileRepository.GetByIdAsync(lionProfileId);
        if (lionProfile == null)
        {
            return false; // Profile not found
        }
        return await _lionProfileRepository.RemoveAsync(lionProfile);
    }
    

    public async Task<List<LionProfile>> SearchProfilesAsync(double weight, string? name, int pageNumber = 1,
        int pageSize = 3)
    {
        return await _lionProfileRepository.SearchAsync(weight, name, pageNumber, pageSize);
    }

    public string CheckRegexProfileName(string name)
    {
        if (name.Length < 4)
        {
            return "Profile name must be at least 4 characters long.";
        }

        bool isStartWord = true;
        foreach (char c in name)
        {
            if (char.IsLetter(c) || char.IsNumber(c))
            {
                if (isStartWord && char.IsLower(c))
                {
                    return "Profile name must start with an uppercase letter.";
                }
                isStartWord = false;
            }
            else if (c == ' ')
            {
                isStartWord = true;
            }
            else
            {
                return "Profile name can only contain letters, number and space.";
            }
        }
        return string.Empty;
    }
}