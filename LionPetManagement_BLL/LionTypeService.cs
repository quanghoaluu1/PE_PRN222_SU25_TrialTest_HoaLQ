using LionPetManagement_DAO;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_BLL;

public class LionTypeService
{
    private readonly LionTypeRepository _lionTypeRepository;
    public LionTypeService(LionTypeRepository lionTypeRepository)
    {
        _lionTypeRepository = lionTypeRepository;
    }

    public List<LionType> GetAllLionTypesAsync()
    {
        return  _lionTypeRepository.GetAll();
    }
}