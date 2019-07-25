using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models
{
    public class FeatureToggle : AbstractFeatureToggle
    {
        private readonly IList<AbstractFeatureToggleRule> _rules;
        public string Namespace { get; }
        public string Environment { get; }
        public bool Activated { get; }
        public int TotalRequestAmount { get; internal set; }
        public int EnabledRequestAmount { get; internal set; }

        public FeatureToggle(string name, string @namespace, string environment, bool activated) : base(name)
        {
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Activated = activated;
            EnrichedState = new NullState(this);

            _rules = new List<AbstractFeatureToggleRule>();
        }

        public override bool IsEnabled()
        {
            if (!Activated) return false;

            return _rules.All(x => x.IsEnabled(this));
        }

        public override void AddRule(AbstractFeatureToggleRule rule)
        {
            _rules.Add(rule);
        }

        public override void Enrich(HttpContext httpContext)
        {
            EnrichedState.Enrich(httpContext);
        }
    }
}
