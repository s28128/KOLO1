using Microsoft.Data.SqlClient;

namespace WebApplication1.Repository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebApplication1.Models;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly string _connectionString;
 
    public MedicamentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
 
    public async Task<Medicament> GetMedicamentByIdAsync(int id)
    {
        Medicament medicament = null;
 
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
 
            var queryString = @"
                    SELECT IdMedicament, Name, Description, Type
                    FROM Medicament
                    WHERE IdMedicament = @Id;
                ";
 
            using (var command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
 
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        medicament = new Medicament
                        {
                            IdMedicament = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Type = reader.GetString(3)
                        };
                    }
                }
            }
        }
 
        return medicament;
    }
}




