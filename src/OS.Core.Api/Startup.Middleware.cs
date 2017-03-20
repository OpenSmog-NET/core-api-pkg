using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using OS.Core.Middleware;

namespace OS.Core
{
    public static class StartupMiddlewareExtensions
    {
        public static IApplicationBuilder UseOpenSmogMiddlewares(this IApplicationBuilder app)
        {
            return app
                .UseCorrelationIdMiddleware()
                .UseRequestLoggingMiddleware();
        }

        public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();

            return app;
        }
    }
}
