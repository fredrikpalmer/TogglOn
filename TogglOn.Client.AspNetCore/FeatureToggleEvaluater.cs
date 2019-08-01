using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Mappers;
using TogglOn.Client.AspNetCore.Models;
using TogglOn.Contract.Commands;

namespace TogglOn.Client.AspNetCore
{
    internal class FeatureToggleEvaluater : IFeatureToggleEvaluater
    {
        private readonly ITogglOnClient _client;
        private readonly ITogglOnContextAccessor _togglOnContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeatureToggleEvaluater(ITogglOnClient client, ITogglOnContextAccessor togglOnContextAccessor, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _togglOnContextAccessor = togglOnContextAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsEnabled(string featureToggleName)
        {
            try
            {
                var toggle = GetFeatureToggle(featureToggleName);

                toggle.Enrich(_httpContextAccessor?.HttpContext);

                var enabled = toggle.IsEnabled();

                IncrementUsageStatisticsAsync(toggle.Name, enabled);

                return enabled;
            }
            catch (Exception)
            {
                //TODO: Log exception

                return false;
            }
        }

        private AbstractFeatureToggle GetFeatureToggle(string featureToggleName)
        {
            var featureToggle = _togglOnContextAccessor.TogglOnContext.FeatureToggles.FirstOrDefault(x => x.Name == featureToggleName);

            if (featureToggle == null) return new NullFeatureToggle();

            var mapper = new FeatureToggleMapper(_togglOnContextAccessor.TogglOnContext.FeatureGroups);
            return mapper.Map(featureToggle);
        }

        private async Task IncrementUsageStatisticsAsync(string featureToggleName, bool enabled)
        {
            var featureToggle = _togglOnContextAccessor.TogglOnContext.FeatureToggles.FirstOrDefault(x => x.Name == featureToggleName);

            if (featureToggle == null) return;

            await _client.ExecuteCommandAsync<IncrementUsageStatisticsCommand, VoidResult>(
                new IncrementUsageStatisticsCommand
                {
                    FeatureToggleId = featureToggle.Id,
                    Enabled = enabled
                });

            featureToggle.IncrementUsageStatistics(enabled);
        }
    }
}
