using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Contract.Models;

namespace TogglOn.Core.Repositories
{
    public interface IFeatureToggleRepository
    {
        Task<IList<FeatureToggleDto>> GetAllAsync(string @namespace = null, string environment = null);
        Task UpdateActivatedAsync(Guid id, bool activated);
        Task IncrementUsageStatisticsAsync(Guid id, bool enabled);
        Task InsertManyAsync(IList<FeatureToggleDto> featureToggles);
    }
}
