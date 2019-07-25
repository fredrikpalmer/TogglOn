using System.Collections.Generic;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Contract.Models;

namespace TogglOn.Contract.Commands
{
    public class InitializeTogglOnCommand : ICommand<VoidResult>
    {
        public string Namespace { get; set; }
        public string Environment { get; set; }
        public IList<FeatureGroupDto> FeatureGroups { get; set; }
        public IList<FeatureToggleDto> FeatureToggles { get; set; }
    }
}
