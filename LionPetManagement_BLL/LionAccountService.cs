using LionPetManagement_DAO;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_BLL;

public class LionAccountService
{
    private readonly LionAccountRepository _lionAccountRepository;
    public LionAccountService(LionAccountRepository lionAccountRepository)
    {
        _lionAccountRepository = lionAccountRepository;
    }
    public async Task<LionAccount?> AuthenticateAsync(string email, string password)
    {
        return await _lionAccountRepository.GetByEmailAndPasswordAsync(email, password);
    }
}