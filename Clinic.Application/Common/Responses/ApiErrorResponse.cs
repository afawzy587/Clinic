namespace Clinic.Application.Common.Responses;

public class ApiErrorResponse
{
     public bool Success { get; init; } = false;

    public string? Message { get; init; }

    public int? Code { get; init; }

    public object? Errors { get; init; }
}
