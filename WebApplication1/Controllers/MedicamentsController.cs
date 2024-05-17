namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Services;
using WebApplication1.Models;
[ApiController]
[Route("api/[controller]")]
public class MedicamentsController : ControllerBase
{
    private readonly IMedicamentService _medicamentService;
 
    public MedicamentsController(IMedicamentService medicamentService)
    {
        _medicamentService = medicamentService;
    }
 
    [HttpGet("{id}")]
    public async Task<ActionResult<Medicament>> GetMedicament(int id)
    {
        var medicament = await _medicamentService.GetMedicamentByIdAsync(id);
 
        if (medicament == null)
        {
            return NotFound();
        }
 
        return Ok(medicament);
    }
}