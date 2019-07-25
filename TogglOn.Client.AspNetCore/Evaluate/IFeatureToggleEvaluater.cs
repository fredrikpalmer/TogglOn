namespace TogglOn.Client.AspNetCore.Evaluate
{
    public interface IFeatureToggleEvaluater
    {
        bool IsEnabled(string featureToggleName);
    }
}