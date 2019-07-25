using Microsoft.AspNetCore.Http;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models
{
    public class NullState : AbstractEnrichedState
    {
        public NullState(AbstractFeatureToggle toggle) : base(null, toggle) { }

        public override void Enrich(HttpContext httpContext)
        {
            _httpContext = httpContext;

            if (HasValue) _toggle.EnrichedState = new EnrichedState(_httpContext, _toggle);
        }
    }
}
