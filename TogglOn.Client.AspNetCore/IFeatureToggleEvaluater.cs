namespace TogglOn.Client.AspNetCore
{
    public interface IFeatureToggleEvaluater
    {
        bool IsEnabled(string featureToggleName);
    }
}