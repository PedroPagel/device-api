using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Device.Domain.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _environment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError("Unexpected error: {e}", e);
                await HandleException(context, e);
            }
        }

        public Task HandleException(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.HttpContext.Request.Path,
                Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500",
                Title = exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            if (_environment.IsDevelopment())
            {
                problemDetails.Detail = JsonConvert.SerializeObject(exception);

                if (exception.InnerException != null)
                {
                    problemDetails.Extensions["code"] = problemDetails.Status;
                    problemDetails.Extensions["Details"] = exception.InnerException;
                }
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
