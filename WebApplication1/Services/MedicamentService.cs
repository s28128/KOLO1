using WebApplication1.Models;

namespace WebApplication1.Services;
using WebApplication1.Dto;
using WebApplication1.Repository;
public interface IMedicamentService
{
   public Task<Medicament> GetMedicamentByIdAsync(int id);
}

    public class MedicamentService : IMedicamentService
    {
        private readonly IMedicamentRepository _medicamentRepository;

        public MedicamentService(IMedicamentRepository medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<Medicament> GetMedicamentByIdAsync(int id)
        {
            return await _medicamentRepository.GetMedicamentByIdAsync(id);
        }
    }
