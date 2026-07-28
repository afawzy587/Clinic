using Microsoft.AspNetCore.Mvc;
using Clinic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Clinic.Application.Interfaces;
using Clinic.Application.DTOs.Patients;
using Clinic.Application.DTOs.Common;
using FluentValidation;
using Clinic.Application.Common;

namespace Clinic.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientsController : BaseApiController
{
    private readonly IPatientService _patientService;

    private readonly IValidator<CreatePatientRequest> _createValidator;

    private readonly IValidator<UpdatePatientRequest> _updateValidator;

    public PatientsController(
        IPatientService patientService,
        IValidator<CreatePatientRequest> createValidator,
        IValidator<UpdatePatientRequest> updateValidator
    )
    {
        _patientService = patientService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await _patientService
                .GetPagedAsync(
                    request,
                    cancellationToken);

            return Ok(result);
        }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken
    )
    {
        var patient = await _patientService.GetByIdAsync(id, cancellationToken);
        if (patient == null)
        {
            return NotFound();
        }
        return OkResponse(
        patient,
        "Patient retrieved successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken
    )
    {
        var validationResult =
        await _createValidator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {

            return ValidationErrorResponse(
                 validationResult.ToDictionary()
            );
        }
        
        var patientResponse = await _patientService.CreateAsync(request, cancellationToken);
        // return CreatedAtAction(nameof(GetById), new { id = patientResponse.Id }, patientResponse);

        return CreatedResponse(
            patientResponse
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdatePatientRequest request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _updateValidator.ValidateAsync(request,cancellationToken);
         if (!validationResult.IsValid)
        {
            return BadRequest(
                new
                {
                    success = false,
                    message = "Validation failed.",
                    errors = validationResult
                        .Errors
                        .Select(error => new
                        {
                            field = error.PropertyName,
                            message = error.ErrorMessage
                        })
                });
        }
        var updated = await _patientService.UpdateAsync(id, request, cancellationToken);
           if (!updated)
            {
                return NotFound(
                    new
                    {
                        success = false,
                        message = $"Patient with ID {id} was not found."
                    });
            }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _patientService
            .DeleteAsync(
                id,
                cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}
