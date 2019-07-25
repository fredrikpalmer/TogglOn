using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Contract.Models;

namespace TogglOn.Core.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<IList<EnvironmentDto>> GetAllAsync();

        Task InsertAsync(EnvironmentDto environment);
    }
}
