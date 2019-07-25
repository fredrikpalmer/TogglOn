using System;
using TogglOn.DependencyInjection.Abstractions;

namespace TogglOn.Core.Configuration
{
    public static class TogglOnOptionsExtensions
    {
        public static ITogglOnOptions UseMongoDb(this ITogglOnOptions options, string connectionString)
        {
            options.UseDataProvider(new MongoDbDataProvider(connectionString ?? throw new ArgumentNullException(nameof(connectionString))));

            return options;
        }
    }
}
