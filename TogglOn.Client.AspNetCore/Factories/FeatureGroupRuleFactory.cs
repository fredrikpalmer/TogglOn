using System;
using System.Collections.Generic;
using System.Linq;
using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Models;
using TogglOn.Client.AspNetCore.Models.Rules;
using TogglOn.Client.AspNetCore.Utils;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Factories
{
    internal class FeatureGroupRuleFactory : IFeatureToggleRuleFactory
    {
        private IList<FeatureGroupDto> _featureGroups;

        public FeatureGroupRuleFactory(IList<FeatureGroupDto> featureGroups)
        {
            _featureGroups = featureGroups ?? throw new ArgumentNullException(nameof(featureGroups));
        }

        public AbstractFeatureToggleRule Create(object[] args)
        {
            var processedArgs = ProcessArguments(args);
            return (AbstractFeatureToggleRule)ObjectActivator.Create<FeatureGroupRule>(processedArgs);
        }

        private object[] ProcessArguments(object[] args)
        {
            var featureGroups = _featureGroups
                .Where(x => args.Contains(x.Name))
                .Select(x => new FeatureGroup
                {
                    Name = x.Name,
                    ClientIps = x.ClientIps,
                    CustomerIds = x.CustomerIds
                })
                .ToList();

            return new object[] { featureGroups };
        }
    }
}
