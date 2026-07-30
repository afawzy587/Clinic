using Clinic.Application.DTOs.Patients;
using Clinic.Application.Common.Localization;
using FluentValidation;
using Clinic.Application.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Clinic.Application.Validators.Patients;

public class CreatePatientRequestValidator :AbstractValidator<CreatePatientRequest>
{
    public readonly IPatientRepository _patientRepository;
    public CreatePatientRequestValidator(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository; 

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(AppText.FirstNameRequired)
            .MaximumLength(50).WithMessage(AppText.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(AppText.LastNameRequired)
            .MaximumLength(50).WithMessage(AppText.LastNameMaxLength);

         RuleFor(x => x)
            .MustAsync(BeUniqueName)
            .WithMessage(AppText.NameAlreadyExists);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(AppText.PhoneRequired)
            .Matches(@"^\+?\d{10,15}$").WithMessage(AppText.PhoneValid);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage(AppText.DateOfBirthPast);
    }

    private async Task<bool> BeUniqueName(
        CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var exists = await _patientRepository
            .ExistsByNameAsync(
                request.FirstName,
                request.LastName,
                null,
                cancellationToken);

        return !exists;
    }
    
}
