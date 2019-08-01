using System.Threading.Tasks;

namespace TogglOn.Client.Abstractions
{
    public interface ITogglOnInitializer
    {
        Task InitializeAsync();
    }
}
