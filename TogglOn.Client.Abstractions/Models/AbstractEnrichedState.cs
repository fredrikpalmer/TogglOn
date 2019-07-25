using Microsoft.AspNetCore.Http;

namespace TogglOn.Client.Abstractions.Models
{
    public abstract class AbstractEnrichedState
    {
        protected HttpContext _httpContext;
        protected AbstractFeatureToggle _toggle;

        public bool HasValue => _httpContext != null;
        public HttpContext HttpContext => _httpContext;

        protected AbstractEnrichedState(HttpContext httpContext, AbstractFeatureToggle toggle)
        {
            _httpContext = httpContext;
            _toggle = toggle;
        }

        public abstract void Enrich(HttpContext httpContext);
    }
}
