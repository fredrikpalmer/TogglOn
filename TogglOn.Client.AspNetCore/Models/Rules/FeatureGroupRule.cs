using System;
using System.Collections.Generic;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models.Rules
{
    internal class FeatureGroupRule : AbstractFeatureToggleRule
    {
        private readonly IList<FeatureGroup> _featureGroups;

        public FeatureGroupRule(IList<FeatureGroup> featureGroups)
        {
            _featureGroups = featureGroups ?? throw new ArgumentNullException(nameof(featureGroups));
        }

        public override bool IsEnabled(AbstractFeatureToggle toggle)
        {
            if (!toggle.EnrichedState.HasValue) return false;

            foreach (var featureGroup in _featureGroups)
            {
                if (featureGroup.ClientIps.Contains(toggle.EnrichedState.HttpContext.Connection.RemoteIpAddress.ToString())) return true;
                if (featureGroup.CustomerIds.Contains(toggle.EnrichedState.HttpContext.User.Identity.Name)) return true;
            }

            return false;
        }
    }
}
