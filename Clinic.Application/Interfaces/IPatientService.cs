using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patients;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces;

public interface IPatientService
{
    Task<PagedResponse<PatientResponse>> GetPagedAsync(
    PaginationRequest request,
    CancellationToken cancellationToken);
    Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken);

    Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<PatientResponse> CreateAsync(CreatePatientRequest request, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        int id,
        UpdatePatientRequest request,
        CancellationToken cancellationToken
    );

    Task<bool> DeleteAsync(
        int id,
         CancellationToken cancellationToken
    );
}
