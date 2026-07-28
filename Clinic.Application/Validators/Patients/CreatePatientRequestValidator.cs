using Clinic.Application.DTOs.Patients;
using Clinic.Application.Common.Localization;
using FluentValidation;

namespace Clinic.Application.Validators.Patients;

public class CreatePatientRequestValidator :AbstractValidator<CreatePatientRequest>
{
    public CreatePatientRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(AppText.FirstNameRequired)
            .MaximumLength(50).WithMessage(AppText.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(AppText.LastNameRequired)
            .MaximumLength(50).WithMessage(AppText.LastNameMaxLength);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(AppText.PhoneRequired)
            .Matches(@"^\+?\d{10,15}$").WithMessage(AppText.PhoneValid);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage(AppText.DateOfBirthPast);
    }
    
}
