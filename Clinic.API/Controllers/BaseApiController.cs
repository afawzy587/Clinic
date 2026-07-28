
using Clinic.Application.Common.Responces;
using Clinic.Application.Common.Responses;
using Clinic.Application.Common.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult OkResponse<T>(
        T data,
        string? message = null)
    {
        return Ok(
            new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
    }

    protected IActionResult OkResponse(
        string message)
    {
        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = message
            });
    }

    protected IActionResult CreatedResponse<T>(
        T data,
        string? message = null)
    {
        message ??= AppText.CreatedSuccessfully;

        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
    }

    protected IActionResult UpdatedResponse(
        string? message = null)
    {
        message ??= AppText.UpdatedSuccessfully;

        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = message
            });
    }

    protected IActionResult DeletedResponse(
        string? message = null)
    {
        message ??= AppText.DeletedSuccessfully;

        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = message
            });
    }

    protected IActionResult BadRequestResponse(
        object? errors = null,
        string? message = null)
    {
        message ??= AppText.BadRequest;

        return BadRequest(
            new ApiErrorResponse
            {
                Message = message,
                Code = 400,
                Errors = errors
            });
    }

    protected IActionResult NotFoundResponse(
        string? message = null)
    {
        message ??= AppText.NotFound;

        return NotFound(
            new ApiErrorResponse
            {
                Message = message,
                Code = 404
            });
    }

    protected IActionResult UnauthorizedResponse(
        string? message = null)
    {
        message ??= AppText.Unauthenticated;

        return Unauthorized(
            new ApiErrorResponse
            {
                Message = message,
                Code = 401
            });
    }

    protected IActionResult ForbiddenResponse(
        string? message = null)
    {
        message ??= AppText.Unauthorized;

        return StatusCode(
            StatusCodes.Status403Forbidden,
            new ApiErrorResponse
            {
                Message = message,
                Code = 401
            });
    }

    protected IActionResult InternalErrorResponse(
        string? message = null)
    {
        message ??= AppText.InternalServerError;

        return StatusCode(
            StatusCodes.Status500InternalServerError,
            new ApiErrorResponse
            {
                Message = message,
                Code = 500
            });
    }

    protected IActionResult ValidationErrorResponse(
        object errors)
    {
        return BadRequest(
            new ApiErrorResponse
            {
                Message = AppText.ValidationFailed,
                Code = 400,
                Errors = errors
            });
    }
}
