using System;
using System.Collections.Generic;

namespace TogglOn.Contract.Models
{
    public class FeatureGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<string> CustomerIds { get; set; }
        public IList<string> ClientIps { get; set; }
    }
}
