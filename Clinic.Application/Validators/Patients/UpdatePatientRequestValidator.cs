using Clinic.Application.DTOs.Patients;
using Clinic.Application.Common.Localization;
using FluentValidation;
using Clinic.Application.Interfaces;

namespace Clinic.Application.Validators.Patients;

public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequest>
{
    private readonly IPatientRepository _patientRepository;
    public UpdatePatientRequestValidator( IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(AppText.FirstNameRequired)
            .MinimumLength(3)
            .MaximumLength(50).WithMessage(AppText.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(AppText.LastNameRequired)
            .MaximumLength(50).WithMessage(AppText.LastNameMaxLength);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(AppText.PhoneRequired)
            .Matches(@"^\+?\d{10,15}$").WithMessage(AppText.PhoneValidLength);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage(AppText.DateOfBirthPast);

        RuleFor(x => x)
            .MustAsync(BeUniqueName)
            .WithMessage(AppText.NameAlreadyExists);
    }

     private async Task<bool> BeUniqueName(
        UpdatePatientRequest request,
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
