using System.Collections.Generic;
using System.Linq;
using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Factories;
using TogglOn.Client.AspNetCore.Models;
using TogglOn.Client.AspNetCore.Utils;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Mappers
{
    internal class FeatureToggleMapper
    {
        private readonly IList<FeatureGroupDto> _featureGroups;

        public FeatureToggleMapper(IList<FeatureGroupDto> featureGroups)
        {
            _featureGroups = featureGroups;
        }

        public FeatureToggle Map(FeatureToggleDto toggleDto)
        {
            var featureToggle = new FeatureToggle(toggleDto.Name, toggleDto.Namespace, toggleDto.Environment, toggleDto.Activated);

            var rules = Map(toggleDto.Rules);

            foreach (var rule in rules)
            {
                featureToggle.AddRule(rule);
            }

            return featureToggle;
        }

        public IEnumerable<AbstractFeatureToggleRule> Map(IList<FeatureToggleRuleDto> rules)
        {
            foreach (var item in rules)
            {
                object[] args = GetArguments(item);

                var factory = GetFactory(item.Name);

                yield return factory.Create(args);
            }
        }

        private object[] GetArguments(FeatureToggleRuleDto item)
        {
            object[] args = new object[item.Properties.Length];
            for (int i = 0; i < item.Properties.Length; i++)
            {
                var property = item.Properties[i];
                if (property is IList<FeatureToggleRuleDto>) args[i] = Map(property as IList<FeatureToggleRuleDto>).ToList();
                else args[i] = property;
            }

            return args;
        }

        private IFeatureToggleRuleFactory GetFactory(string name)
        {
            switch (name)
            {
                case FeatureToggleRules.FeatureGroup:
                    return new FeatureGroupRuleFactory(_featureGroups);
                case FeatureToggleRules.Percentage:
                    return new PercentageRuleFactory();
                case FeatureToggleRules.QueryString:
                    return new QueryStringRuleFactory();
                case FeatureToggleRules.WhenAll:
                    return new WhenAllRuleFactory();
                case FeatureToggleRules.WhenAny:
                    return new WhenAnyRuleFactory();
                default:
                    return new NullFeatureToggleRuleFactory();
            }
        }
    }
}
