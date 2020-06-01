using HeroesCup.Web.Common.Middlewares.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace HeroesCup.Web.Common.Extensions
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnhandledExceptionLogging(this IApplicationBuilder builder) => 
            builder.UseMiddleware<LogUnhandledExceptionMiddleware>();
    }
}
