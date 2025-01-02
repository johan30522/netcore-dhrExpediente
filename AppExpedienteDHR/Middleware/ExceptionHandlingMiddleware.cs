
using System.Net;

namespace AppExpedienteDHR.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Generar un identificador único para el error
            var errorId = Guid.NewGuid().ToString();

            // Mensaje técnico para TI (detalles del error)
            var technicalDetails = exception.ToString(); // Incluye stack trace y detalles completos

            // Obtener el tipo de excepción como código de error
            var exceptionCode = exception.GetType().Name;

            // Registrar el error en los logs
            _logger.Error(exception, "Error ID: {ErrorId}. Detalles: {Message}", errorId, exception.Message);

            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Respuesta para AJAX (JSON)
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = "Se produjo un error técnico. Contacte a soporte técnico.",
                    ErrorId = errorId,
                    ExceptionCode = exceptionCode,
                    TechnicalDetails = technicalDetails
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            else
            {
                // Redirigir a la página de error con el mensaje técnico y el identificador del error
                var errorMessage = Uri.EscapeDataString("Se produjo un error técnico. Por favor, proporcione los detalles a TI.");
                var encodedDetails = Uri.EscapeDataString(technicalDetails); // Codificar detalles técnicos para la URL
                var encodedCode = Uri.EscapeDataString(exceptionCode); // Codificar el código de la excepción
                context.Response.Redirect($"/Client/Home/Error?errorMessage={errorMessage}&errorId={errorId}&details={encodedDetails}&code={encodedCode}");
            }
        }

    }
}
