using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TogglOn.Client.Abstractions.Command;
using TogglOn.Contract.Commands;
using TogglOn.Contract.Models;
using TogglOn.Core.Repositories;

namespace TogglOn.Core.CommandHandlers
{
    internal class InitializeTogglOnCommandHandler : ICommandHandler<InitializeTogglOnCommand, VoidResult>
    {
        private readonly IInitializeDbRepository _initializeDbRepository;
        private readonly INamespaceRepository _namespaceRepository;
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IFeatureGroupRepository _featureGroupRepository;
        private readonly IFeatureToggleRepository _featureToggleRepository;

        public InitializeTogglOnCommandHandler(
            IInitializeDbRepository initializeDbRepository,
            INamespaceRepository namespaceRepository,
            IEnvironmentRepository environmentRepository,
            IFeatureGroupRepository featureGroupRepository,
            IFeatureToggleRepository featureToggleRepository)
        {
            _initializeDbRepository = initializeDbRepository;
            _namespaceRepository = namespaceRepository;
            _environmentRepository = environmentRepository;
            _featureGroupRepository = featureGroupRepository;
            _featureToggleRepository = featureToggleRepository;
        }

        public async Task<VoidResult> HandleAsync(InitializeTogglOnCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            await _initializeDbRepository.CreateAsync();

            var namespaceCollectionTask = _namespaceRepository.GetAllAsync();
            var environmentCollectionTask = _environmentRepository.GetAllAsync();
            var featureGroupCollectionTask = _featureGroupRepository.GetAllAsync();
            var featureToggleCollectionTask = _featureToggleRepository.GetAllAsync(command.Namespace, command.Environment);

            var namespaceCollection = await namespaceCollectionTask;
            var environmentCollection = await environmentCollectionTask;
            var featureGroupCollection = await featureGroupCollectionTask;
            var featureToggleCollection = await featureToggleCollectionTask;

            var addNamespaceTask = AddNamespaceIfNotExistsAsync(namespaceCollection, command.Namespace);
            var addEnvironmentTask = AddEnvironmentIfNotExistsAsync(environmentCollection, command.Environment);
            var addFeatureGroupTask = AddFeatureGroupIfNotExistsAsync(featureGroupCollection, command.FeatureGroups);
            var addFeatureToggleTask = AddFeatureToggleIfNotExistsAsync(featureToggleCollection, command.FeatureToggles);

            await addNamespaceTask;
            await addEnvironmentTask;
            await addFeatureGroupTask;
            await addFeatureToggleTask;

            return new VoidResult();
        }

        private async Task AddNamespaceIfNotExistsAsync(IList<NamespaceDto> namespaceCollection, string @namespace)
        {
            if (!namespaceCollection.Any(x => x.Name == @namespace))
                await _namespaceRepository.InsertAsync(new NamespaceDto { Name = @namespace });
        }

        private async Task AddEnvironmentIfNotExistsAsync(IList<EnvironmentDto> environmentCollection, string environment)
        {
            if (!environmentCollection.Any(x => x.Name == environment))
                await _environmentRepository.InsertAsync(new EnvironmentDto { Name = environment });
        }

        private async Task AddFeatureGroupIfNotExistsAsync(IList<FeatureGroupDto> featureGroupCollection, IList<FeatureGroupDto> featureGroups)
        {
            var notExistingFeatureGroups = !featureGroupCollection.Any() 
                ? featureGroups.Select(x => x.Name).ToList()
                : featureGroups
                .Select(x => x.Name)
                .Except(featureGroupCollection.Select(x => x.Name))
                .ToList();

            if (notExistingFeatureGroups.Any())
            {
                await _featureGroupRepository.InsertManyAsync(featureGroups.Where(x => notExistingFeatureGroups.Contains(x.Name)).ToList());
            }
        }

        private async Task AddFeatureToggleIfNotExistsAsync(IList<FeatureToggleDto> featureToggleCollection, IList<FeatureToggleDto> featureToggles)
        {
            var notExistingFeatureToggles = !featureToggleCollection.Any() 
                ? featureToggles.Select(x => x.Name).ToList()
                : featureToggles
                .Select(x => x.Name)
                .Except(featureToggleCollection.Select(x => x.Name))
                .ToList();

            if (notExistingFeatureToggles.Any())
            {
                await _featureToggleRepository.InsertManyAsync(featureToggles.Where(x => notExistingFeatureToggles.Contains(x.Name)).ToList());
            }
        }
    }
}
