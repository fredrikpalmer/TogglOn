using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TogglOn.Core.Repositories;

namespace TogglOn.Admin.Controllers
{
    [Route("api/[controller]")]
    public class FeatureGroupsController : Controller
    {
        private readonly IFeatureGroupRepository _repository;
        private readonly ILogger<FeatureGroupsController> _logger;

        public FeatureGroupsController(IFeatureGroupRepository repository, ILogger<FeatureGroupsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var featureGroups = await _repository.GetAllAsync();

            return Ok(featureGroups);
        }
    }
}