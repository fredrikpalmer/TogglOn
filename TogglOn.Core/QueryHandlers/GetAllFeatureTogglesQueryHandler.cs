using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Query;
using TogglOn.Contract.Models;
using TogglOn.Contract.Queries;
using TogglOn.Core.Repositories;

namespace TogglOn.Core.QueryHandlers
{
    internal class GetAllFeatureTogglesQueryHandler : IQueryHandler<GetAllFeatureTogglesQuery, IList<FeatureToggleDto>>
    {
        private readonly IFeatureToggleRepository _repository;

        public GetAllFeatureTogglesQueryHandler(IFeatureToggleRepository repository)
        {
            _repository = repository;
        }

        public Task<IList<FeatureToggleDto>> HandleAsync(GetAllFeatureTogglesQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return _repository.GetAllAsync(query.Namespace, query.Environment);
        }
    }
}
