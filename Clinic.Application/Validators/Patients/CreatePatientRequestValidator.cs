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
            .Matches(@"^(?=(?:.*[a-zA-Z]){3})[a-zA-Z0-9\s@]+$")
            .NotEmpty()
            .MinimumLength(3).MaximumLength(50)
            .WithName(AppText.FirstName);

        RuleFor(x => x.LastName)
            .Matches(@"^(?=(?:.*[a-zA-Z]){3})[a-zA-Z0-9\s@]+$")
            .WithMessage(AppText.LastNameInvalid)
            .NotEmpty().WithMessage(AppText.LastNameRequired)
            .MinimumLength(3).MaximumLength(50).WithMessage(AppText.LastNameMaxLength);

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
