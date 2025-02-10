using DTOs.SuiteType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.SuiteType;
using Services.SuiteTypes;

namespace Controller.SuiteType
{
    [ApiController]
    [Route("api/suites/[controller]")]
    [Authorize]
    public class SuiteTypesController : ControllerBase
    {
        private readonly ISuiteTypeService _suiteTypeService;
        public SuiteTypesController(ISuiteTypeService suiteTypeService)
        {
            _suiteTypeService = suiteTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<Suite>> CreateSuiteType([FromBody] SuiteTypeDTO suiteTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var suiteType = await _suiteTypeService.CreateSuiteTypeAsync(suiteTypeDto);
                return CreatedAtAction(nameof(GetSuiteTypeById), new { id = suiteType.Id }, suiteType);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suite>> GetSuiteTypeById(int id)
        {
            var suiteType = await _suiteTypeService.GetSuiteTypeByIdAsync(id);
            if (suiteType == null)
            {
                return NotFound();
            }
            return Ok(suiteType);
        }
    }
}