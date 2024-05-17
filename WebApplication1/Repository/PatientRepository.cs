using Microsoft.Data.SqlClient;
using WebApplication1.Repository;

public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;
 
        public PatientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
 
        public async Task<bool> DeletePatientByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        var deletePrescriptionMedicamentQuery = @"
                            DELETE FROM Prescription_Medicament
                            WHERE IdPrescription IN (
                                SELECT IdPrescription
                                FROM Prescription
                                WHERE IdPatient = @Id
                            );
                        ";
 
                        using (var deletePrescriptionMedicamentCommand = new SqlCommand(deletePrescriptionMedicamentQuery, connection, transaction))
                        {
                            deletePrescriptionMedicamentCommand.Parameters.AddWithValue("@Id", id);
                            await deletePrescriptionMedicamentCommand.ExecuteNonQueryAsync();
                        }
 
                        
                        var deletePrescriptionQuery = @"
                            DELETE FROM Prescription
                            WHERE IdPatient = @Id;
                        ";
 
                        using (var deletePrescriptionCommand = new SqlCommand(deletePrescriptionQuery, connection, transaction))
                        {
                            deletePrescriptionCommand.Parameters.AddWithValue("@Id", id);
                            await deletePrescriptionCommand.ExecuteNonQueryAsync();
                        }
 
                        
                        var deletePatientQuery = @"
                            DELETE FROM Patient
                            WHERE IdPatient = @Id;
                        ";
 
                        using (var deletePatientCommand = new SqlCommand(deletePatientQuery, connection, transaction))
                        {
                            deletePatientCommand.Parameters.AddWithValue("@Id", id);
                            var rowsAffected = await deletePatientCommand.ExecuteNonQueryAsync();
 
                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
