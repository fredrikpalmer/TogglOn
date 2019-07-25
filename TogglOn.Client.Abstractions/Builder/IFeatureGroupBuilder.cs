namespace TogglOn.Client.Abstractions.Builder
{
    public interface IFeatureGroupBuilder
    {
        IFeatureGroupBuilder WithGroup(string groupName);
        IFeatureGroupBuilder WithCustomerIds(params string[] customerIds);
        IFeatureGroupBuilder WithClientIps(params string[] clientIps);
    }
}