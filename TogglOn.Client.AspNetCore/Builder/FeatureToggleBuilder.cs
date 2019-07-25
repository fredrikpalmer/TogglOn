using System;
using System.Collections.Generic;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore.Utils;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Builder
{
    internal class FeatureToggleBuilder : IFeatureToggleBuilder, IFeatureToggleRuleBuilder
    {
        private readonly string _namespace;
        private readonly string _environment;

        private readonly IList<FeatureToggleDto> _featureToggles;

        public FeatureToggleBuilder(string @namespace, string environment)
        {
            _namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _featureToggles = new List<FeatureToggleDto>();
        }

        public IFeatureToggleRuleBuilder WithToggle(string featureToggleName, bool activated = false)
        {
            if (featureToggleName == null) throw new ArgumentNullException(nameof(featureToggleName));

            _featureToggles.Add(new FeatureToggleDto
            {
                Name = featureToggleName,
                Namespace = _namespace,
                Environment = _environment,
                Activated = activated
            });

            return this;
        }

        public void WhenAll(Action<IFeatureToggleRulePicker> includeRulesAction)
        {
            AddGroup(includeRulesAction, FeatureToggleRules.WhenAll);
        }

        public void WhenAny(Action<IFeatureToggleRulePicker> includeRulesAction)
        {
            AddGroup(includeRulesAction, FeatureToggleRules.WhenAny);
        }

        private void AddGroup(Action<IFeatureToggleRulePicker> includeRulesAction, string featureGroupName)
        {
            if (includeRulesAction == null) throw new ArgumentNullException(nameof(includeRulesAction));

            var picker = new FeatureToggleRulePicker();

            includeRulesAction(picker);

            var toggle = GetToggle();
            toggle.Rules.Add(new FeatureToggleRuleDto(featureGroupName, picker.Rules));
        }

        private FeatureToggleDto GetToggle()
        {
            return _featureToggles[_featureToggles.Count - 1];
        }

        internal IList<FeatureToggleDto> Build()
        {
            return _featureToggles;
        }
    }
}
