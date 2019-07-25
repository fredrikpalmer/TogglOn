using System;
using System.Collections.Generic;
using System.Linq;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models.Rules
{
    internal class WhenAnyRule : AbstractFeatureToggleRule
    {
        public IList<AbstractFeatureToggleRule> Rules { get; }

        public WhenAnyRule(IList<AbstractFeatureToggleRule> rules)
        {
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public override bool IsEnabled(AbstractFeatureToggle toggle)
        {
            return Rules.Any(x => x.IsEnabled(toggle));
        }
    }
}
