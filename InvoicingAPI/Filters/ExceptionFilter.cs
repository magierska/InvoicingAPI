using InvoicingAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InvoicingAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;
    private readonly IHostEnvironment hostEnvironment;

    public ExceptionFilter(ILogger<ExceptionFilter> logger, IHostEnvironment hostEnvironment)
    {
        this.logger = logger;
        this.hostEnvironment = hostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, context.Exception.Message);

        switch (context.Exception)
        {
            case RecordNotFoundException:
                HandleRecordNotFoundException(context);
                break;
            default:
                HandleUnknownException(context);
                break;
        }
    }

    private void HandleRecordNotFoundException(ExceptionContext context)
    {
        context.Result = new ContentResult
        {
            Content = hostEnvironment.IsDevelopment() ? context.Exception.ToString() : "Record was not found in database.",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        context.Result = new ContentResult
        {
            Content = hostEnvironment.IsDevelopment() ? context.Exception.ToString() : "Something went wrong. Please contact the development team.",
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}