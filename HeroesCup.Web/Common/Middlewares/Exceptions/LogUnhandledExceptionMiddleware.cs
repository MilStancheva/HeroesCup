using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HeroesCup.Web.Common.Middlewares.Exceptions
{
    public class LogUnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public LogUnhandledExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger(string.Empty);
                logger.LogError(ex, string.Empty);
                throw;
            }
        }
    }
}