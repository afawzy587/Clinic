using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Interfaces;
using Clinic.Application.DTOs.Patients;
using Clinic.Application.DTOs.Common;
using FluentValidation;
using Clinic.Application.Common.Localization;

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
            [FromQuery] PatientFilterRequest request,
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
            return NotFoundResponse(AppText.PatientWithIdNotFound(id));
        }
        return OkResponse(
        patient,
        AppText.PatientRetrievedSuccessfully);
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
            return ValidationErrorResponse(validationResult.ToDictionary());
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
            return ValidationErrorResponse(validationResult.ToDictionary());
        }
        var updated = await _patientService.UpdateAsync(id, request, cancellationToken);
           if (!updated)
            {
                return NotFoundResponse(AppText.PatientWithIdNotFound(id));
            }
        return UpdatedResponse();
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
            return NotFoundResponse(AppText.PatientWithIdNotFound(id));
        }

        return NoContent();
    }
    
}
