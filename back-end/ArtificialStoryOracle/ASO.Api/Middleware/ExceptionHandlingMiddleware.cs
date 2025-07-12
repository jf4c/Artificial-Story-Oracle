using System.Net;
using System.Text.Json;
using ASO.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ASO.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        var errorResponse = new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Success = false
        };

        switch (exception)
        {
                            case ValidationException validationException:
                statusCode = validationException.StatusCode;
                errorResponse.Error = new ErrorDetails
                {
                    Message = validationException.Message,
                    StatusCode = (int)statusCode,
                    Details = validationException.Errors
                };
                break;
            case DomainException domainException:
                statusCode = domainException.StatusCode;
                errorResponse.Error = new ErrorDetails
                {
                    Message = domainException.Message,
                    StatusCode = (int)statusCode
                };
                break;

            default:
                errorResponse.Error = new ErrorDetails
                {
                    Message = _environment.IsDevelopment() 
                        ? exception.Message 
                        : "Ocorreu um erro interno no servidor.",
                    StatusCode = (int)statusCode
                };

                if (_environment.IsDevelopment())
                {
                    if (exception.StackTrace != null)
                        errorResponse.Error.StackTrace = exception.StackTrace;
                }
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsJsonAsync(errorResponse, options);
    }
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public ErrorDetails Error { get; set; } = new ErrorDetails();
    public string TraceId { get; set; } = string.Empty;
}

public class ErrorDetails
{
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string StackTrace { get; set; } = string.Empty;
    public IEnumerable<string> Details { get; set; } = new List<string>();
}
