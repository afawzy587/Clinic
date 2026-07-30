using Clinic.Application.DTOs.Common;

namespace Clinic.Application.DTOs.Patients;

public class PatientFilterRequest : PaginationRequest
{
    public string? Phone { get; set; }

    public DateTime? DateOfBirthFrom { get; set; }

    public DateTime? DateOfBirthTo { get; set; }

}
