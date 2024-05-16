using CapitalTask.Models;
using Microsoft.AspNetCore.Mvc;
using CapitalTask.Services;

namespace CapitalTaskTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly ICosmosService _cosmo;
        private readonly ILogger<ProgramsController> _logger;

        public ProgramsController(ICosmosService cosmo, ILogger<ProgramsController> logger)
        {
            _cosmo = cosmo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create(_Program req)
        {
            try
            {
                _logger.LogInformation("before inserting Program");
                req.id = Guid.NewGuid().ToString();
                await _cosmo.AddItemAsync(req, "programs");

                _logger.LogInformation("after inserting Program");

                return Ok(new Response(true, "Program created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating Program");
                return Problem(detail: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] _Program req)
        {
            try
            {
                _logger.LogInformation($"before updating Program : {id}, {req}");
                await _cosmo.UpdateItemAsync(id, req, "programs");

                _logger.LogInformation("after updating Program");

                return Ok(new Response(true, "Program updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating Program");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                _Program program = await _cosmo.GetItemAsync<_Program>(id, "programs");
                return Ok(program);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Program");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<_Program> programs = await _cosmo.GetItemsAsync<_Program>("SELECT * FROM c", "programs");
                return Ok(programs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Program");
                return Problem(detail: ex.Message);
            }
        }
    }
}
