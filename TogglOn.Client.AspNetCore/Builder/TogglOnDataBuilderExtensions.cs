using Microsoft.AspNetCore.Builder;
using System;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore.Middleware;

namespace TogglOn.Client.AspNetCore.Builder
{
    public static class TogglOnDataBuilderExtensions
    {
        public static IApplicationBuilder UseTogglOnClient(this IApplicationBuilder app, Action<ITogglOnDataBuilder> action)
        {
            var builder = new TogglOnDataBuilder(app.ApplicationServices);

            action(builder);

            var initializer = builder.Build();

            initializer.InitializeAsync().Wait();

            app.UseMiddleware<RequestTogglOnContextMiddleware>();

            return app;
        }
    }
}
