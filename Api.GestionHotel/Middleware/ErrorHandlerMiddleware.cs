using Api.Manager.Application.Wrappers;
using Api.Manager.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Api.GestionHotel.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ResponseError<string>() { Success = false, Message = ex.Message };
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(ex, "An unhandled exception occurred.");

                switch (ex)
                {
                    case ValidationExceptions e:
                        responseModel.Errors = e.Errors;

                        break;
                    case ApiException e:
                        responseModel.Message = "An error occurred while executing the task.";
                        responseModel.Errors = new List<string> { e.Message };
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
