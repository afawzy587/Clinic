using System.Globalization;

namespace Clinic.API.Middleware;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cultureName = ResolveCulture(context.Request.Headers.AcceptLanguage.ToString());
        var culture = CultureInfo.GetCultureInfo(cultureName);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }

    private static string ResolveCulture(string? acceptLanguage)
    {
        if (string.IsNullOrWhiteSpace(acceptLanguage))
        {
            return "en";
        }

        return acceptLanguage.Contains("ar", StringComparison.OrdinalIgnoreCase)
            ? "ar"
            : "en";
    }
}
