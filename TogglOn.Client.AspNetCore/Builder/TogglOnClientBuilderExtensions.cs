using Microsoft.AspNetCore.Builder;
using System;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore.Builder;

namespace TogglOn.Core.Configuration
{
    public static class TogglOnClientBuilderExtensions
    {
        public static IApplicationBuilder UseTogglOnClient(this IApplicationBuilder app, Action<ITogglOnClientBuilder> action)
        {
            var clientBuilder = new TogglOnClientBuilder(app.ApplicationServices);

            action(clientBuilder);

            var client = clientBuilder.Build();

            client.StartAsync();

            return app;
        }
    }
}
