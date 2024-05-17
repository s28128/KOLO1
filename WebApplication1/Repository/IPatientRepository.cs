namespace WebApplication1.Repository;

public interface IPatientRepository
{
    Task<bool> DeletePatientByIdAsync(int id);
}