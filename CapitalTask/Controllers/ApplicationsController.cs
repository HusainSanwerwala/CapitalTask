using CapitalTask.Services;
using CapitalTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapitalTaskTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ICosmosService _cosmo;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(ICosmosService cosmo, ILogger<ApplicationsController> logger)
        {
            _cosmo = cosmo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Application req)
        {
            try
            {
                req.id = Guid.NewGuid().ToString();
                await _cosmo.AddItemAsync(req, "applications");
                return Ok(new Response(true, "Application created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating Application");
                return Problem(detail: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Application req)
        {
            try
            {
                await _cosmo.UpdateItemAsync(id, req, "applications");
                return Ok(new Response(true, "Application updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Application");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                Application application = await _cosmo.GetItemAsync<Application>(id, "applications");
                return Ok(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Application");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Application> applications = await _cosmo.GetItemsAsync<Application>("SELECT * FROM c", "applications");
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Applications");
                return Problem(detail: ex.Message);
            }
        }
    }
}