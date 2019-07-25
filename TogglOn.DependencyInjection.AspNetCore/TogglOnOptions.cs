using System;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.DependencyInjection.AspNetCore
{
    internal class TogglOnOptions : ITogglOnOptions
    {
        public IDataProvider Provider { get; internal set; }

        public void UseDataProvider(IDataProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }
}
