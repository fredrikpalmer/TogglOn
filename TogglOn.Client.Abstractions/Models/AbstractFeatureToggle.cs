using Microsoft.AspNetCore.Http;
using System;

namespace TogglOn.Client.Abstractions.Models
{
    public abstract class AbstractFeatureToggle
    {
        public string Name { get; }
        public AbstractEnrichedState EnrichedState { get; set; }

        protected AbstractFeatureToggle(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public abstract void AddRule(AbstractFeatureToggleRule rule);

        //TODO: Remove httpcontext as dependency
        public abstract void Enrich(HttpContext httpContext);
        public abstract bool IsEnabled();
    }
}
