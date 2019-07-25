using System;
using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore;
using TogglOn.Client.AspNetCore.Builder;
using TogglOn.Client.AspNetCore.Evaluate;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.Core.Configuration
{
    public static class TogglOnBuilderExtensions
    {
        public static ITogglOnBuilder AddClient(this ITogglOnBuilder builder, Action<ITogglOnClientOptions> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var services = builder.Services;

            var options = new TogglOnClientOptions(services);
            configure(options);

            services.AddSingleton<ITogglOnContextAccessor, TogglOnContextAccessor>();
            services.AddSingleton<ITogglOnClient, TogglOnClient>();

            services.AddSingleton<IFeatureToggleEvaluater, FeatureToggleEvaluater>();

            return builder;
        }
    }
}
