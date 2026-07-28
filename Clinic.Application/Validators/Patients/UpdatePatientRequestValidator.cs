using Clinic.Application.DTOs.Patients;
using FluentValidation;

namespace Clinic.Application.Validators.Patients;

public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequest>
{

    public UpdatePatientRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be valid and contain 10 to 15 digits.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
    }
    
}
