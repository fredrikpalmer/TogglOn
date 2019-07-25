using System.Threading.Tasks;

namespace TogglOn.Core.Repositories
{
    public interface IInitializeDbRepository
    {
        Task CreateAsync();
    }
}
