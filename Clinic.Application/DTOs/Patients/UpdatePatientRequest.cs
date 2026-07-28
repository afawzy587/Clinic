namespace Clinic.Application.DTOs.Patients;

public class UpdatePatientRequest
{
     public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }
}
