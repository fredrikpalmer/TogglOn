using System;

namespace TogglOn.Client.Abstractions.Builder
{
    public interface ITogglOnDataBuilder
    {
        ITogglOnDataBuilder DeclareNamespace(string @namespace);
        ITogglOnDataBuilder DeclareEnvironment(string environment);
        ITogglOnDataBuilder DeclareFeatureGroups(Action<IFeatureGroupBuilder> builder);
        ITogglOnDataBuilder DeclareFeatureToggles(Action<IFeatureToggleBuilder> builder);
        ITogglOnInitializer Build();
    }
}