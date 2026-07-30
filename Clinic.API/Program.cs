using Clinic.API.Middleware;
using Clinic.Application;
using Clinic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Application Layer
builder.Services.AddApplication();

// Infrastructure Layer
builder.Services.AddInfrastructure( builder.Configuration);

// API
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.AllMiddleware();

app.MapControllers();

app.Run();