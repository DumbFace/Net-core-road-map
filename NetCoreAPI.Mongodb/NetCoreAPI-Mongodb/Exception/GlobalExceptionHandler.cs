using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace Test.HandleException
{

    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            string[] exceptionAsStrings = ["Null", "Sql"];

            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);


            if (exceptionAsStrings.Any(exceptionAsString => exception.GetType().ToString().Contains(exceptionAsString)))
            {

                _logger.LogInformation("---------------------------Write Log FIle");
                Log.Error($"------Type Exception {exception.GetType().ToString()}: {exception.Message}");
            }
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(exception.Message, cancellationToken);

            return true;
        }
    }
}
