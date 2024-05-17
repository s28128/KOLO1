namespace WebApplication1.Services;

public interface IPatientService
{
    Task<bool> DeletePatientByIdAsync(int id);
}