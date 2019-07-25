using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TogglOn.Core.Repositories;

namespace TogglOn.Admin.Controllers
{
    [Route("api/[controller]")]
    public class FeatureTogglesController : Controller
    {
        private readonly IFeatureToggleRepository _repository;
        private readonly ILogger<FeatureTogglesController> _logger;

        public FeatureTogglesController(IFeatureToggleRepository repository, ILogger<FeatureTogglesController> logger)
        {

            _repository = repository;
            _logger = logger;
        }

        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var featureToggles = await _repository.GetAllAsync();

            return Ok(featureToggles);
        }

        [Route("{id}"), HttpPut]
        public async Task<IActionResult> UpdateActivatedAsync(Guid id, [FromBody] bool activated)
        {
            await _repository.UpdateActivatedAsync(id, activated);

            return Ok();
        }
    }
}