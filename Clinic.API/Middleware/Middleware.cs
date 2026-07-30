namespace Clinic.API.Middleware;

public static class Middleware
{
   public static void AllMiddleware(this WebApplication app)
    {
       app.UseMiddleware<RequestCultureMiddleware>();
       app.UseMiddleware<ExceptionHandlingMiddleware>(); 
    } 
}
