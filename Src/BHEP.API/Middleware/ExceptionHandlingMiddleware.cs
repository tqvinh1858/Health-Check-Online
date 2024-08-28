﻿using System.Text.Json;
using BHEP.Domain.Exceptions;

namespace BHEP.API.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => this.logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new
        {
            isSuccess = false,
            statusCode,
            message = exception.Message
        };

        if (statusCode == 500)
            logger.LogError(exception, exception.Message);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    //private static IReadOnlyCollection<Application.Exceptions.ValidationError> GetErrors(Exception exception)
    //{
    //    IReadOnlyCollection<Application.Exceptions.ValidationError> errors = null;

    //    if (exception is Application.Exceptions.ValidationException validationException)
    //    {
    //        errors = validationException.Errors;
    //    }

    //    return errors;
    //}
}
