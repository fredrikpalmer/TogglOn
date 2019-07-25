using System;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Contract.Commands;
using TogglOn.Core.Repositories;

namespace TogglOn.Core.CommandHandlers
{
    internal class IncrementUsageStatisticsCommandHandler : ICommandHandler<IncrementUsageStatisticsCommand, VoidResult>
    {
        private readonly IFeatureToggleRepository _repository;

        public IncrementUsageStatisticsCommandHandler(IFeatureToggleRepository repository)
        {
            _repository = repository;
        }

        public async Task<VoidResult> HandleAsync(IncrementUsageStatisticsCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            await _repository.IncrementUsageStatisticsAsync(command.FeatureToggleId, command.Enabled);

            return new VoidResult();
        }
    }
}
