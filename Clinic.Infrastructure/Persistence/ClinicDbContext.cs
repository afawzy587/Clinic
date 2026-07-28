namespace Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Domain.Entities;
public class ClinicDbContext : DbContext
{
    public ClinicDbContext(
        DbContextOptions<ClinicDbContext> options)
         : base(options
    )
    {
        
    }
    public DbSet<Patient> Patients => Set<Patient>();
}
