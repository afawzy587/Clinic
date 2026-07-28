namespace Clinic.Application.Services;

using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patients;

using Clinic.Application.Interfaces;
using Clinic.Domain.Entities;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    public PatientService(
        IPatientRepository patientRepository
    )
    {
        _patientRepository = patientRepository;
    }

    public async Task<PagedResponse<PatientResponse>> GetPagedAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var (patients, totalCount) = await _patientRepository.GetPagedAsync(request, cancellationToken);

        var patientResponses = patients.Select(MapToResponse).ToList();
        return new PagedResponse<PatientResponse>
        {
            Items = patientResponses,
            PageSize = request.GetSafePageSize(),
            PageNumber = request.GetSafePageNumber(),
            TotalCount = totalCount
        };
    }

    public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _patientRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _patientRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<PatientResponse> CreateAsync(CreatePatientRequest request, CancellationToken cancellationToken)
    {
        var patient = new Patient(
             request.FirstName,
             request.LastName,
             request.Phone,
            request.DateOfBirth
        );

        await _patientRepository.AddAsync(patient, cancellationToken);
        await _patientRepository.SaveChangesAsync(cancellationToken);

        return MapToResponse(patient);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdatePatientRequest request,
        CancellationToken cancellationToken
    )
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient == null)
        {
            return false;
        }
        patient.Update(
            request.FirstName,
            request.LastName,
            request.Phone,
            request.DateOfBirth
        );

        _patientRepository.Update(patient);
        await _patientRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
         CancellationToken cancellationToken
    )
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient == null)
        {
            return false;
        }

        patient.Deactivate();
        _patientRepository.Update(patient);
        await _patientRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static PatientResponse MapToResponse(
        Patient patient)
    {
        return new PatientResponse
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Phone = patient.Phone,
            DateOfBirth = patient.DateOfBirth,
            IsActive = patient.IsActive
        };
    }
}
