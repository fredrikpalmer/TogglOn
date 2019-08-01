using System;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models.Rules
{
    internal class NullRule : AbstractFeatureToggleRule
    {
        public override bool IsEnabled(AbstractFeatureToggle toggle)
        {
            return false;
        }
    }
}
