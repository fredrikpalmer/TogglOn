using System;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions;
using TogglOn.Contract.Commands;

namespace TogglOn.Client.AspNetCore
{
    internal class TogglOnInitializer : ITogglOnInitializer
    {
        private readonly ITogglOnClient _client;
        private readonly ITogglOnContextAccessor _togglOnContextAccessor;

        public TogglOnInitializer(ITogglOnClient client, ITogglOnContextAccessor togglOnContextAccessor)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _togglOnContextAccessor = togglOnContextAccessor ?? throw new ArgumentNullException(nameof(togglOnContextAccessor));
        }

        public Task InitializeAsync()
        {
            return _client.ExecuteCommandAsync<InitializeTogglOnCommand, VoidResult>(new InitializeTogglOnCommand
            {
                Namespace = _togglOnContextAccessor.TogglOnContext?.Namespace,
                Environment = _togglOnContextAccessor.TogglOnContext?.Environment,
                FeatureGroups = _togglOnContextAccessor.TogglOnContext?.FeatureGroups,
                FeatureToggles = _togglOnContextAccessor.TogglOnContext?.FeatureToggles
            });
        }
    }
}
