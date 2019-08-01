using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Models.Rules;

namespace TogglOn.Client.AspNetCore.Factories
{
    internal class NullFeatureToggleRuleFactory : IFeatureToggleRuleFactory
    {
        public AbstractFeatureToggleRule Create(object[] args)
        {
            return new NullRule();
        }
    }
}
