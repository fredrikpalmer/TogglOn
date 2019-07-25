using Microsoft.Extensions.DependencyInjection;
using System;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.DependencyInjection.AspNetCore
{
    internal class DefaultServiceConfigurator : IServiceConfigurator
    {
        private readonly IServiceCollection _services;

        public DefaultServiceConfigurator(IServiceCollection services)
        {
            _services = services;
        }

        void IServiceConfigurator.AddSingleton<TService, TImplementation>()
        {
            AddSingleton(typeof(TService), typeof(TImplementation));
        }

        public void AddSingleton(Type serviceType, Type implementationType)
        {
            _services.AddSingleton(serviceType, implementationType);
        }

        public void AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            _services.AddSingleton(serviceType, implementationFactory);
        }
    }
}
