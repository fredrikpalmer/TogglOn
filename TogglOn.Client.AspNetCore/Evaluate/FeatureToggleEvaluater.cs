using Microsoft.AspNetCore.Http;
using System;
using TogglOn.Client.Abstractions;

namespace TogglOn.Client.AspNetCore.Evaluate
{
    internal class FeatureToggleEvaluater : IFeatureToggleEvaluater
    {
        private readonly ITogglOnClient _client;
        private readonly IHttpContextAccessor _contextAccessor;

        public FeatureToggleEvaluater(ITogglOnClient client, IHttpContextAccessor contextAccessor)
        {
            _client = client;
            _contextAccessor = contextAccessor;
        }

        public bool IsEnabled(string featureToggleName)
        {
            try
            {
                var toggle = _client.GetFeatureToggle(featureToggleName);

                toggle.Enrich(_contextAccessor?.HttpContext);

                var enabled = toggle.IsEnabled();

                _client.IncrementUsageStatisticsAsync(featureToggleName, enabled);

                return enabled;
            }
            catch (Exception)
            {
                //TODO: Log exception

                return false;
            }
        }
    }
}
