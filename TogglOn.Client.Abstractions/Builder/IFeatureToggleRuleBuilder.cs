using System;

namespace TogglOn.Client.Abstractions.Builder
{

    public interface IFeatureToggleRuleBuilder
    {
        void WhenAll(Action<IFeatureToggleRulePicker> includeRulesAction);
        void WhenAny(Action<IFeatureToggleRulePicker> includeRulesAction);
    }
}
