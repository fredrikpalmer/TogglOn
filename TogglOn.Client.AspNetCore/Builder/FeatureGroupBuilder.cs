
using System;
using System.Collections.Generic;
using System.Linq;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Contract.Models;

namespace TogglOn.Client.AspNetCore.Builder
{
    internal class FeatureGroupBuilder : IFeatureGroupBuilder
    {
        private readonly IList<FeatureGroupDto> _featureGroups;

        public FeatureGroupBuilder()
        {
            _featureGroups = new List<FeatureGroupDto>();
        }

        public IFeatureGroupBuilder WithGroup(string groupName)
        {
            _featureGroups.Add(new FeatureGroupDto
            {
                Name = groupName ?? throw new ArgumentNullException(nameof(groupName))
            });

            return this;
        }

        public IFeatureGroupBuilder WithCustomerIds(params string[] customerIds)
        {
            var group = GetGroup();

            group.CustomerIds = customerIds.ToList();

            return this;
        }

        public IFeatureGroupBuilder WithClientIps(params string[] clientIps)
        {
            var group = GetGroup();

            group.ClientIps = clientIps.ToList();

            return this;
        }

        private FeatureGroupDto GetGroup()
        {
            return _featureGroups[_featureGroups.Count - 1];
        }

        internal IList<FeatureGroupDto> Build()
        {
            return _featureGroups;
        }
    }
}