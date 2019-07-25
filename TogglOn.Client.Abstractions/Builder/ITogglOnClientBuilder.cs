using System;

namespace TogglOn.Client.Abstractions.Builder
{
    public interface ITogglOnClientBuilder
    {
        ITogglOnClientBuilder DeclareNamespace(string @namespace);
        ITogglOnClientBuilder DeclareEnvironment(string environment);
        ITogglOnClientBuilder DeclareFeatureGroups(Action<IFeatureGroupBuilder> builder);
        ITogglOnClientBuilder DeclareFeatureToggles(Action<IFeatureToggleBuilder> builder);
        ITogglOnClient Build();
    }
}