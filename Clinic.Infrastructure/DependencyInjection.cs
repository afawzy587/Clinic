using Clinic.Infrastructure.Persistence;
using Clinic.Infrastructure.Repositories;
using Clinic.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection"
                )
            )
        );

        services.AddScoped<
            IPatientRepository,
            PatientRepository>();

        return services;
    }
}