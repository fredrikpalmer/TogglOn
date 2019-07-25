using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.Abstractions
{
    public interface ITogglOnClient
    {
        Task StartAsync();

        AbstractFeatureToggle GetFeatureToggle(string featureToggleName);

        Task IncrementUsageStatisticsAsync(string featureToggleName, bool enabled);
    }
}
