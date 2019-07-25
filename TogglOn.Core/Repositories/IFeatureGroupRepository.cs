using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Contract.Models;

namespace TogglOn.Core.Repositories
{
    public interface IFeatureGroupRepository
    {
        Task<IList<FeatureGroupDto>> GetAllAsync();

        Task InsertManyAsync(IList<FeatureGroupDto> featureGroups);
    }
}
