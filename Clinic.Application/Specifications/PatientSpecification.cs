using Clinic.Application.DTOs.Patients;
using Clinic.Domain.Entities;

namespace Clinic.Application.Specifications;

public class PatientSpecification
{
    public IQueryable<Patient> Apply(
        IQueryable<Patient> query,
        PatientFilterRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.Phone.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            query = query.Where(x =>
                x.Phone == request.Phone);
        }

        if (request.DateOfBirthFrom.HasValue)
        {
            query = query.Where(x =>
                x.DateOfBirth >=
                request.DateOfBirthFrom.Value);
        }

        if (request.DateOfBirthTo.HasValue)
        {
            query = query.Where(x =>
                x.DateOfBirth <=
                request.DateOfBirthTo.Value);
        }

        return query;
    }
}
