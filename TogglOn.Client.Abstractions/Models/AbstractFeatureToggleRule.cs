namespace TogglOn.Client.Abstractions.Models
{
    public abstract class AbstractFeatureToggleRule
    {
        public abstract bool IsEnabled(AbstractFeatureToggle toggle);
    }
}
