using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Factories
{
    internal interface IFeatureToggleRuleFactory
    {
        AbstractFeatureToggleRule Create(object[] args);
    }
}
