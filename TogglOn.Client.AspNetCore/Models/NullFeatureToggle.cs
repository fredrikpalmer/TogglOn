using Microsoft.AspNetCore.Http;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models
{
    internal class NullFeatureToggle : AbstractFeatureToggle
    {
        public NullFeatureToggle() : base(nameof(NullFeatureToggle)) { }

        public override void AddRule(AbstractFeatureToggleRule rule) { }

        public override void Enrich(HttpContext httpContext) { }

        public override bool IsEnabled()
        {
            return false;
        }
    }
}
