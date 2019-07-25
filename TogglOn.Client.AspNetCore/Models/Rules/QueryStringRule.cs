using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models.Rules
{
    internal class QueryStringRule : AbstractFeatureToggleRule
    {
        private const string Enabled = "enabled";

        public override bool IsEnabled(AbstractFeatureToggle toggle)
        {
            if (!toggle.EnrichedState.HasValue) return false;
            if (!toggle.EnrichedState.HttpContext.Request.Query.ContainsKey(toggle.Name)) return false;

            return toggle.EnrichedState.HttpContext.Request.Query[toggle.Name].ToString().Equals(Enabled);
        }
    }
}
