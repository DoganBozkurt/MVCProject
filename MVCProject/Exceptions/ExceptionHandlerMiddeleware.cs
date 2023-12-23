using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MVCProject.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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

                // Hata oluştuğunda istisna durumunu logla
                _logger.LogError(ex, "Bir hata oluştu: {Message}", ex.Message);

                // Özel hata sayfasına yönlendirme
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Yönlendirme işlemi
                context.Response.Redirect("/ErrorPage/Error");

                // Hata işlendiği için talebi yeniden işlemeyi durdur
                return;
            }
        }
    }
}
