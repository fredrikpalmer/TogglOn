using System.Threading.Tasks;

namespace TogglOn.Client.Abstractions.Command
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
