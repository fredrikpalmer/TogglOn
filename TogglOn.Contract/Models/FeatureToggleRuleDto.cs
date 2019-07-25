using System.Collections.Generic;

namespace TogglOn.Contract.Models
{
    public class FeatureToggleRuleDto
    {
        public string Name { get; set; }

        public object[] Properties { get; set; }

        public FeatureToggleRuleDto(string name, params object[] properties)
        {
            Name = name;
            Properties = properties;
        }
    }
}
