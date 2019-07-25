using System;
using System.Collections.Generic;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore.Utils;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Builder
{
    internal class FeatureToggleRulePicker : IFeatureToggleRulePicker
    {
        public IList<FeatureToggleRuleDto> Rules { get; set; }
        public FeatureToggleRulePicker()
        {
            Rules = new List<FeatureToggleRuleDto>();
        }

        public void WithFeatureGroup(params string[] featureGroupNames)
        {
            if (featureGroupNames == null) throw new ArgumentNullException(nameof(featureGroupNames));

            AddRule(FeatureToggleRules.FeatureGroup, featureGroupNames);
        }

        public void WithPercentage(int percentage)
        {
            if (percentage < 0 || percentage > 100) throw new ArgumentOutOfRangeException(nameof(percentage), percentage, "Value of percentage must be in range 0 and 100");

            AddRule(FeatureToggleRules.Percentage, percentage);
        }

        public void WithQueryString()
        {
            AddRule(FeatureToggleRules.QueryString);
        }

        private void AddRule(string name, params object[] properties)
        {
            //TODO: throw exception when adding same rule multiple times

            Rules.Add(new FeatureToggleRuleDto(name, properties));
        }
    }
}
