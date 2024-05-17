namespace WebApplication1.Services;
using WebApplication1.Models;
using WebApplication1.Repository;
public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
 
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
 
    public async Task<bool> DeletePatientByIdAsync(int id)
    {
        return await _patientRepository.DeletePatientByIdAsync(id);
    }
}