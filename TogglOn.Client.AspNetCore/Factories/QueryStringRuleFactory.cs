using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Models.Rules;
using TogglOn.Client.AspNetCore.Utils;

namespace TogglOn.Client.AspNetCore.Factories
{
    internal class QueryStringRuleFactory : IFeatureToggleRuleFactory
    {
        public AbstractFeatureToggleRule Create(object[] args)
        {
            return (AbstractFeatureToggleRule)ObjectActivator.Create<QueryStringRule>(args);
        }
    }
}
