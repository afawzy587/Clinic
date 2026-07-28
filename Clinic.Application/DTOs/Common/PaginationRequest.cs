namespace Clinic.Application.DTOs.Common;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }

     public int GetSafePageNumber()
    {
        return Math.Max(PageNumber, 1);
    }

    public int GetSafePageSize()
    {
        return Math.Min(
            Math.Max(PageSize, 1),
            100
        );
    }
}
