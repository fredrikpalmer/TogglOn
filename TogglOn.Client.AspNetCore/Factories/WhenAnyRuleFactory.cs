using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Models.Rules;
using TogglOn.Client.AspNetCore.Utils;

namespace TogglOn.Client.AspNetCore.Factories
{
    internal class WhenAnyRuleFactory : IFeatureToggleRuleFactory
    {
        public AbstractFeatureToggleRule Create(object[] args)
        {
            return (AbstractFeatureToggleRule)ObjectActivator.Create<WhenAnyRule>(args);
        }
    }
}
