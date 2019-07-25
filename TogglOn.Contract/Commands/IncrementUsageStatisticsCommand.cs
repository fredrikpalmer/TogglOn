using System;
using TogglOn.Client.Abstractions.Command;

namespace TogglOn.Contract.Commands
{
    public class IncrementUsageStatisticsCommand : ICommand<VoidResult>
    {
        public Guid FeatureToggleId { get; set; }
        public bool Enabled { get; set; }
    }
}
