using Clinic.Application.DTOs.Common;
using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync(
        CancellationToken cancellationToken);
    Task<(List<Patient> items, int TotalCount)> GetPagedAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);

    Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task AddAsync(Patient patient, CancellationToken cancellationToken);

    void Update(Patient patient);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
    string firstName,
    string lastName,
    int? excludeId,
    CancellationToken cancellationToken);

    
}
