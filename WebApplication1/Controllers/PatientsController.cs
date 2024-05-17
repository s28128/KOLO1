using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Services;
namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
 
    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var result = await _patientService.DeletePatientByIdAsync(id);
 
        if (!result)
        {
            return NotFound();
        }
 
        return NoContent();
    }
}