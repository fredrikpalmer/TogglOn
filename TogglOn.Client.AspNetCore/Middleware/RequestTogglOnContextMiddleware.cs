using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions;
using TogglOn.Contract.Models;
using TogglOn.Contract.Queries;

namespace TogglOn.Client.AspNetCore.Middleware
{
    internal class RequestTogglOnContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTogglOnContextMiddleware> _logger;
        private readonly ITogglOnClient _client;
        private readonly ITogglOnContextAccessor _togglOnContextAccessor;

        private string _namespace;
        private string _environment;
        private IList<FeatureGroupDto> _featureGroups;
        private IList<FeatureToggleDto> _featureToggles;

        private TimeSpan _refreshTime;
        private TimeSpan _currentTime;
        private TimeSpan _refreshInterval = TimeSpan.FromMinutes(15);

        public RequestTogglOnContextMiddleware(RequestDelegate next, ILogger<RequestTogglOnContextMiddleware> logger, ITogglOnClient client, ITogglOnContextAccessor togglOnContextAccessor)
        {
            _next = next;
            _logger = logger;
            _client = client;
            _togglOnContextAccessor = togglOnContextAccessor;

            _namespace = _togglOnContextAccessor.TogglOnContext?.Namespace;
            _environment = _togglOnContextAccessor.TogglOnContext?.Environment;
            _featureGroups = _togglOnContextAccessor.TogglOnContext?.FeatureGroups;
            _featureToggles = _togglOnContextAccessor.TogglOnContext?.FeatureToggles;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _currentTime = DateTime.Now.TimeOfDay;

            if (ShouldRefresh())
            {
                _refreshTime = _currentTime;

                _logger.LogInformation("Refreshing toggles");

                var stopWatch = Stopwatch.StartNew();

                var featureGroupsTask = _client.ExecuteQueryAsync<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>(
                    new GetAllFeatureGroupsQuery());

                var featureTogglesTask = _client.ExecuteQueryAsync<GetAllFeatureTogglesQuery, IList<FeatureToggleDto>>(
                    new GetAllFeatureTogglesQuery
                    {
                        Namespace = _namespace,
                        Environment = _environment
                    });

                _featureGroups = await featureGroupsTask ?? _featureGroups;
                _featureToggles = await featureTogglesTask ?? _featureToggles;

                stopWatch.Stop();

                _logger.LogInformation($"Finished refreshing toggles in {stopWatch.ElapsedMilliseconds} ms");
            }

            _togglOnContextAccessor.TogglOnContext = new TogglOnContext(_namespace, _environment, _featureGroups, _featureToggles);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private bool ShouldRefresh()
        {
            if (_refreshTime == default) return true;

            var elapsedTime = _currentTime - _refreshTime;
            if (elapsedTime >= _refreshInterval) return true;

            return false;
        }
    }
}
