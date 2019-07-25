using System.Collections.Generic;
using TogglOn.Client.Abstractions.Query;
using TogglOn.Contract.Models;

namespace TogglOn.Contract.Queries
{
    public class GetAllFeatureTogglesQuery : IQuery<IList<FeatureToggleDto>>
    {
        public string Namespace { get; set; }
        public string Environment { get; set; }
    }
}
