namespace Clinic.Application.DTOs.Patients;

public class PatientResponse
{
     public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public bool IsActive { get; set; }

}
