using Clinic.API.Middleware;
using Clinic.Application.Interfaces;
using Clinic.Application.Services;
using Clinic.Infrastructure.Persistence;
using Clinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Clinic.Application.Validators.Patients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<
    CreatePatientRequestValidator
    >();
    
builder.Services.AddControllers();

builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"
        )
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

// Add services to the container.

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// Configure the HTTP request pipeline.


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestCultureMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.UseExceptionHandler();

app.Run();

