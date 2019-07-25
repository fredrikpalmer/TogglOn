namespace TogglOn.Client.Abstractions.Builder
{
    public interface IFeatureToggleBuilder
    {
        IFeatureToggleRuleBuilder WithToggle(string featureToggleName, bool activated = false);
    }
}