using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions;
using TogglOn.Client.Abstractions.Models;
using TogglOn.Client.AspNetCore.Mappers;
using TogglOn.Client.AspNetCore.Models;
using TogglOn.Contract.Commands;
using TogglOn.Contract.Models;
using TogglOn.Contract.Queries;

namespace TogglOn.Client.AspNetCore
{
    internal class TogglOnClient : ITogglOnClient, IDisposable
    {
        private static readonly object _myLock = new object();

        private Timer _timer;

        private readonly ILogger<TogglOnClient> _logger;
        private readonly ITogglOnDataClient _toggleDataClient;
        private readonly ITogglOnContextAccessor _togglOnContextAccessor;

        public TogglOnClient(ILogger<TogglOnClient> logger, ITogglOnDataClient toggleDataClient, ITogglOnContextAccessor togglOnContextAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toggleDataClient = toggleDataClient ?? throw new ArgumentNullException(nameof(toggleDataClient));
            _togglOnContextAccessor = togglOnContextAccessor;
        }

        protected Task InitializeAsync()
        {
            return _toggleDataClient.ExecuteCommandAsync<InitializeTogglOnCommand, VoidResult>(new InitializeTogglOnCommand
            {
                Namespace = _togglOnContextAccessor.TogglOnContext?.Namespace,
                Environment = _togglOnContextAccessor.TogglOnContext?.Environment,
                FeatureGroups = _togglOnContextAccessor.TogglOnContext?.FeatureGroups,
                FeatureToggles = _togglOnContextAccessor.TogglOnContext?.FeatureToggles
            });
        }

        public async Task StartAsync()
        {
            await InitializeAsync();

            _timer = new Timer(Refresh, null, TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(10));
        }

        private async void Refresh(object state)
        {
            _logger.LogInformation("Refreshing toggles");

            var stopWatch = Stopwatch.StartNew();

            var featureGroupsTask = _toggleDataClient.ExecuteQueryAsync<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>(
                new GetAllFeatureGroupsQuery());

            var featureTogglesTask = _toggleDataClient.ExecuteQueryAsync<GetAllFeatureTogglesQuery, IList<FeatureToggleDto>>(
                new GetAllFeatureTogglesQuery
                {
                    Namespace = _togglOnContextAccessor.TogglOnContext?.Namespace,
                    Environment = _togglOnContextAccessor.TogglOnContext?.Environment
                });

            var @namespace = _togglOnContextAccessor.TogglOnContext.Namespace;
            var environment = _togglOnContextAccessor.TogglOnContext.Environment;
            var featureGroups = await featureGroupsTask;
            var featureToggles = await featureTogglesTask;

            lock (_myLock)
            {
                _togglOnContextAccessor.TogglOnContext = new TogglOnContext(@namespace, environment, featureGroups, featureToggles);
            }

            stopWatch.Stop();

            _logger.LogInformation($"Finished refreshing toggles in {stopWatch.ElapsedMilliseconds} ms");
        }

        public AbstractFeatureToggle GetFeatureToggle(string featureToggleName)
        {
            lock (_myLock)
            {
                var featureToggle = _togglOnContextAccessor.TogglOnContext.FeatureToggles.FirstOrDefault(x => x.Name == featureToggleName);

                if (featureToggle == null) return new NullFeatureToggle();

                var mapper = new FeatureToggleMapper(_togglOnContextAccessor.TogglOnContext.FeatureGroups);
                return mapper.Map(featureToggle);
            }
        }

        public async Task IncrementUsageStatisticsAsync(string featureToggleName, bool enabled)
        {
            FeatureToggleDto featureToggle = null;
            lock (_myLock)
            {
                featureToggle = _togglOnContextAccessor.TogglOnContext.FeatureToggles.FirstOrDefault(x => x.Name == featureToggleName);
            }

            if (featureToggle == null) return;

            await _toggleDataClient.ExecuteCommandAsync<IncrementUsageStatisticsCommand, VoidResult>(
                new IncrementUsageStatisticsCommand
                {
                    FeatureToggleId = featureToggle.Id,
                    Enabled = enabled
                });

            featureToggle.IncrementUsageStatistics(enabled);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer.Dispose();
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ToggleRefresher()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
