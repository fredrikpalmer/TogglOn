using System;

namespace TogglOn.DependencyInjection.Abstractions
{
    public interface IServiceConfigurator
    {
        void AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void AddSingleton(Type serviceType, Type implementationType);
        void AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory);
    }
}
