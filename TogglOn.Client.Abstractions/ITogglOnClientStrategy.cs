using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Client.Abstractions.Query;

namespace TogglOn.Client.Abstractions
{
    public interface ITogglOnClientStrategy
    {
        Task<TResult> ExecuteQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    }
}
