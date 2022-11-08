using System.Net;
using System.Reflection;
using System.Security.Authentication;
using Refit;

namespace LCARS.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var _ = ex switch
            {
                AuthenticationException => CreateResponse(context, ex, HttpStatusCode.Unauthorized),
                ArgumentException => CreateResponse(context, ex, HttpStatusCode.BadRequest),
                InvalidOperationException => CreateResponse(context, ex, (HttpStatusCode)412),
                AccessViolationException => CreateResponse(context, ex, HttpStatusCode.Forbidden),
                HttpRequestException or ApiException => CreateResponse(context, ex, HttpStatusCode.BadGateway),
                _ => CreateResponse(context, ex, HttpStatusCode.InternalServerError),
            };
        }
    }

    private static HttpContext CreateResponse(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
    {
        var responseBody = new
        {
            Code = ((int)httpStatusCode).ToString(),
            Status = httpStatusCode.ToString(),
            Title = ex.GetBaseException().Message,
            Detail = ex is ApiException exception ? exception.Content : ex.GetBaseException().Message,
            Source = Assembly.GetEntryAssembly()?.GetName().Name
        }.ToString();

        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.WriteAsync(responseBody);

        return context;
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
