using Clinic.Application.Interfaces;
using Clinic.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<
            PatientService>();

        // Application Services
        services.AddScoped<
            IPatientService,
            PatientService>();

        return services;
    }
}