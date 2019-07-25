using System.Collections.Generic;
using System.Threading.Tasks;
using TogglOn.Contract.Models;

namespace TogglOn.Core.Repositories
{
    public interface INamespaceRepository
    {
        Task<IList<NamespaceDto>> GetAllAsync();

        Task InsertAsync(NamespaceDto @namespace);
    }
}
