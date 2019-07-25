using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Query;
using TogglOn.Contract.Models;
using TogglOn.Contract.Queries;
using TogglOn.Core.Repositories;

namespace TogglOn.Core.QueryHandlers
{
    internal class GetAllFeatureGroupsQueryHandler : IQueryHandler<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>
    {
        private readonly IFeatureGroupRepository _repository;

        public GetAllFeatureGroupsQueryHandler(IFeatureGroupRepository repository)
        {
            _repository = repository;
        }

        public Task<IList<FeatureGroupDto>> HandleAsync(GetAllFeatureGroupsQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return _repository.GetAllAsync();
        }
    }
}
