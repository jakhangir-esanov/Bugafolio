using FinancialPortfolioManagement.Service.Exceptions;
using FinancialPortfolioManagement.WebApi.Models;

namespace FinancialPortfolioManagement.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate request;
    private readonly ILogger<Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware> logger;
    public ExceptionHandlerMiddleware(RequestDelegate request, ILogger<Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware> logger)
    {
        this.request = request;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await this.request.Invoke(httpContext);
        }
        catch (NotFoundException ex)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }
        catch (AlreadyExistException ex)
        {
            httpContext.Response.StatusCode = 403;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }
    }
}
