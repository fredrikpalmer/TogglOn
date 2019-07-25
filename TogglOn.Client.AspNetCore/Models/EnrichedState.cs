using Microsoft.AspNetCore.Http;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models
{
    public class EnrichedState : AbstractEnrichedState
    {
        public EnrichedState(HttpContext httpContext, AbstractFeatureToggle toggle) : base(httpContext, toggle) { }

        public override void Enrich(HttpContext httpContext)
        {
            _httpContext = httpContext;

            if (!HasValue) _toggle.EnrichedState = new NullState(_toggle);
        }
    }
}
