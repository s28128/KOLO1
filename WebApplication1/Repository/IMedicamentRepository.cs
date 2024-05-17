using WebApplication1.Models;

namespace WebApplication1.Repository;
using WebApplication1.Dto;
public interface IMedicamentRepository
{
    Task<Medicament> GetMedicamentByIdAsync(int id);
}