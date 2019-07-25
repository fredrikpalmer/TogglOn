using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Builder
{
    public class TogglOnClientBuilder : ITogglOnClientBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        private string _namespace;
        private string _environment;

        private Action<IFeatureGroupBuilder> _featureGroupBuilder;
        private Action<IFeatureToggleBuilder> _featureToggleBuilder;

        public TogglOnClientBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ITogglOnClientBuilder DeclareNamespace(string @namespace)
        {
            _namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));

            return this;
        }

        public ITogglOnClientBuilder DeclareEnvironment(string environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            return this;
        }

        public ITogglOnClientBuilder DeclareFeatureGroups(Action<IFeatureGroupBuilder> featureGroupBuilder)
        {
            _featureGroupBuilder = featureGroupBuilder ?? throw new ArgumentNullException(nameof(featureGroupBuilder));
            return this;
        }

        public ITogglOnClientBuilder DeclareFeatureToggles(Action<IFeatureToggleBuilder> featureToggleBuilder)
        {
            _featureToggleBuilder = featureToggleBuilder ?? throw new ArgumentNullException(nameof(featureToggleBuilder));
            return this;
        }

        public ITogglOnClient Build()
        {
            var featureGroups = BuildFeatureGroups();
            var featureToggles = BuildFeatureToggles();

            var contextAccessor = _serviceProvider.GetService<ITogglOnContextAccessor>();
            contextAccessor.TogglOnContext = new TogglOnContext(_namespace, _environment, featureGroups, featureToggles);

            return _serviceProvider.GetService<ITogglOnClient>();
        }

        private IList<FeatureGroupDto> BuildFeatureGroups()
        {
            var builder = new FeatureGroupBuilder();

            _featureGroupBuilder(builder);

            return builder.Build();
        }

        private IList<FeatureToggleDto> BuildFeatureToggles()
        {
            var builder = new FeatureToggleBuilder(_namespace, _environment);

            _featureToggleBuilder(builder);

            return builder.Build();
        }
    }
}
