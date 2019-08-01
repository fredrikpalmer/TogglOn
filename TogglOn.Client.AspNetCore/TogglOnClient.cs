using System;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Client.Abstractions.Query;

namespace TogglOn.Client.AspNetCore
{
    internal class TogglOnClient : ITogglOnClient
    {
        private readonly ITogglOnClientStrategy _clientStrategy;

        public TogglOnClient(ITogglOnClientStrategy togglOnClientStrategy)
        {
            _clientStrategy = togglOnClientStrategy ?? throw new ArgumentNullException(nameof(togglOnClientStrategy));
        }

        public Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            return _clientStrategy.ExecuteCommandAsync<TCommand, TResult>(command);
        }

        public Task<TResult> ExecuteQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return _clientStrategy.ExecuteQueryAsync<TQuery, TResult>(query);
        }
    }
}
