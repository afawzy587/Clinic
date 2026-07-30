
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
        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponse<T>
            {
                Success = true,
                Message = message ?? AppText.CreatedSuccessfully,
                Data = data
            });
    }

    protected IActionResult UpdatedResponse( string? message = null){
        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = message ?? AppText.UpdatedSuccessfully
            });
    }

    protected IActionResult DeletedResponse(
        string? message = null)
    {
        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = message ?? AppText.DeletedSuccessfully
            });
    }

    protected IActionResult BadRequestResponse(
        object? errors = null,
        string? message = null)
    {
        return BadRequest(
            new ApiErrorResponse
            {
                Success = false,
                Message = message ?? AppText.BadRequest,
                Code = StatusCodes.Status400BadRequest,
                Errors = errors
            });
    }

    protected IActionResult ValidationErrorResponse(
        IDictionary<string, string[]> errors)
    {
        var firstMessage = errors
            .SelectMany(x => x.Value)
            .FirstOrDefault()
            ?? AppText.ValidationFailed;

        return BadRequest(
            new ApiErrorResponse
            {
                Success = false,
                Message = firstMessage,
                Code = StatusCodes.Status400BadRequest,
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
                Success = false,
                Message = message,
                Code = StatusCodes.Status404NotFound
            });
    }

    protected IActionResult UnauthorizedResponse(string? message = null){
        message ??= AppText.Unauthorized;
        return Unauthorized(
            new ApiErrorResponse
            {
                Success = false,
                Message = message,
                Code = StatusCodes.Status401Unauthorized
            });
    }

    protected IActionResult ForbiddenResponse(string? message = null){
        return StatusCode(
            StatusCodes.Status403Forbidden,
            new ApiErrorResponse
            {
                Success = false,
                Message = message ?? AppText.Unauthenticated,
                Code = StatusCodes.Status403Forbidden
            });
    }

    protected IActionResult InternalErrorResponse(
        string? message = null){
        return StatusCode(
            StatusCodes.Status500InternalServerError,
            new ApiErrorResponse
            {
                Success = false,
                Message = message ?? AppText.InternalServerError,
                Code = StatusCodes.Status500InternalServerError
            });
    }
}
