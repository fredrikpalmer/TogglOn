namespace TogglOn.Client.AspNetCore
{
    public interface IFeatureToggleEvaluator
    {
        bool IsEnabled(string featureToggleName);
    }
}