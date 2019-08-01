using System;
using System.Collections.Generic;

namespace TogglOn.Contract.Models
{
    public class FeatureToggleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Environment { get; set; }
        public bool Activated { get; set; }
        public int TotalRequestAmount { get; internal set; } 
        public int EnabledRequestAmount { get; internal set; } 
        public IList<FeatureToggleRuleDto> Rules { get; set; }

        public FeatureToggleDto()
        {
            Rules = new List<FeatureToggleRuleDto>();
        }

        public void IncrementUsageStatistics(bool enabled)
        {
            TotalRequestAmount += 1;

            if (enabled) EnabledRequestAmount += 1;
        }
    }
}
