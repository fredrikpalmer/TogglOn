using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.Client.AspNetCore.Builder
{
    internal class TogglOnClientOptions : ITogglOnClientOptions
    {
        private readonly IServiceConfigurator _services;

        public TogglOnClientOptions(IServiceConfigurator services)
        {
            _services = services;
        }

        public void UseInProcClient()
        {
            _services.AddSingleton<ITogglOnClientStrategy, InProcTogglOnClientStrategy>();
        }
    }
}
