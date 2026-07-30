using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patients;
using Clinic.Application.Interfaces;
using Clinic.Application.Specifications;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    public readonly ClinicDbContext _dbContext;

    public PatientRepository(
        ClinicDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

   public async Task<(List<Patient> items, int TotalCount)> GetPagedAsync(
        PatientFilterRequest request, 
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Patients
            .AsNoTracking()
            .Where(x => x.IsActive);

        var specification = new PatientSpecification();

        query = specification.Apply(query,request);

        var totalCount = await query.CountAsync(
            cancellationToken);

        query = request.SortBy?.ToLower() switch
        {
            "firstname" => request.SortDescending
                ? query.OrderByDescending(x => x.FirstName)
                : query.OrderBy(x => x.FirstName),

            "lastname" => request.SortDescending
                ? query.OrderByDescending(x => x.LastName)
                : query.OrderBy(x => x.LastName),

            "dateofbirth" => request.SortDescending
                ? query.OrderByDescending(x => x.DateOfBirth)
                : query.OrderBy(x => x.DateOfBirth),

            _ => query.OrderBy(x => x.Id)
        };

        var pageNumber =
            request.GetSafePageNumber();

        var pageSize =
            request.GetSafePageSize();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }


    public async Task<List<Patient>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Patients
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _dbContext.Patients.AddAsync(patient, cancellationToken);
    }

    public void Update(Patient patient)
    {
        _dbContext.Patients.Update(patient);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(
    string firstName,
    string lastName,
    int? excludeId,
    CancellationToken cancellationToken)
    {
        return await _dbContext.Patients
            .AsNoTracking()
            .AnyAsync(
                x =>
                    x.FirstName == firstName &&
                    x.LastName == lastName &&
                    (!excludeId.HasValue ||
                    x.Id != excludeId.Value),
                cancellationToken);
    }

}
