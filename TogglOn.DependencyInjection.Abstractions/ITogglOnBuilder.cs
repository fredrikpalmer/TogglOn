namespace TogglOn.DependencyInjection.Abstractions
{
    public interface ITogglOnBuilder
    {
        IServiceConfigurator Services { get; }
    }
}
