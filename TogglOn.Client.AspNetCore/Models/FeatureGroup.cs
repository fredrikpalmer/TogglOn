using System.Collections.Generic;

namespace TogglOn.Client.AspNetCore.Models
{
    public class FeatureGroup
    {
        public string Name { get; set; }
        public IList<string> CustomerIds { get; set; }
        public IList<string> ClientIps { get; set; }
    }
}
