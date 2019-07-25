namespace TogglOn.Client.Abstractions.Builder
{
    public interface IFeatureToggleRulePicker
    {
        void WithFeatureGroup(params string[] featureGroupNames);
        void WithPercentage(int percentage);
        void WithQueryString();
    }
}
