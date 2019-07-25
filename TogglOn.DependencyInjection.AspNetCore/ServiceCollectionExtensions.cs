using Microsoft.Extensions.DependencyInjection;
using System;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.DependencyInjection.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static ITogglOnBuilder AddTogglOnCore(this IServiceCollection services, Action<ITogglOnOptions> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var options = new TogglOnOptions();

            configure(options);

            options.Provider.Configure(new DefaultServiceConfigurator(services));

            return new TogglOnBuilder(new DefaultServiceConfigurator(services));
        }
    }
}
