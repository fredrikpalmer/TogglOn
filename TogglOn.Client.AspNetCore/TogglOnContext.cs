using System.Collections.Generic;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore
{
    public class TogglOnContext
    {
        public string Namespace { get; }
        public string Environment { get; }
        public IList<FeatureGroupDto> FeatureGroups { get; }
        public IList<FeatureToggleDto> FeatureToggles { get; }

        public TogglOnContext(string @namespace, string environment, IList<FeatureGroupDto> featureGroups, IList<FeatureToggleDto> featureToggles)
        {
            Namespace = @namespace;
            Environment = environment;
            FeatureGroups = featureGroups;
            FeatureToggles = featureToggles;
        }
    }
}
