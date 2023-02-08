using Confluent.Kafka;
using System.Net;

namespace CartApi.CustomExceptionMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is KafkaException kafkaException)
        {
            Console.WriteLine($"\r\n\r\n\r\nError in kafka!, Error: {kafkaException.Message}\r\n\r\n\r\n");
        }
        else
        {
            Console.WriteLine($"Error!, Error: {exception.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}

