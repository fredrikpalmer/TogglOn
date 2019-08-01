using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TogglOn.Client.Abstractions.Query;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Client.Abstractions;

namespace TogglOn.Client.AspNetCore
{
    internal class InProcTogglOnClientStrategy : ITogglOnClientStrategy
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InProcTogglOnClientStrategy> _logger;

        public InProcTogglOnClientStrategy(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = (ILogger<InProcTogglOnClientStrategy>)_serviceProvider.GetService(typeof(ILogger<InProcTogglOnClientStrategy>));
        }

        public Task<TResult> ExecuteQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            _logger.LogInformation($"Executing query: {typeof(TQuery).Name}");

            try
            {
                var handler = (IQueryHandler<TQuery, TResult>)_serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

                return handler.HandleAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed executing query: {typeof(TQuery).Name}");
                return Task.FromException<TResult>(ex);
            }
        }

        public Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            _logger.LogInformation($"Executing command: {typeof(TCommand).Name}");

            try
            {
                var handler = (ICommandHandler<TCommand, TResult>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));

                return handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed executing command: {typeof(TCommand).Name}");
                return Task.FromException<TResult>(ex);
            }
        }
    }
}
