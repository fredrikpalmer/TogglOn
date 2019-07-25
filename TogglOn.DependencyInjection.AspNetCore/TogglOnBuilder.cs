using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.DependencyInjection.AspNetCore
{
    internal class TogglOnBuilder : ITogglOnBuilder
    {
        public IServiceConfigurator Services { get; }

        public TogglOnBuilder(IServiceConfigurator services)
        {
            Services = services;
        }
    }
}
