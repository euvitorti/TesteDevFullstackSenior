using DTOs.Motels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Motels;
using Services.Motels;

namespace Controller.Motels
{
    [ApiController]
    [Route("api/motels/[controller]")]
    [Authorize]
    public class MotelsController : ControllerBase
    {
        private readonly IMotelService _motelService;

        public MotelsController(IMotelService motelService)
        {
            _motelService = motelService;
        }

        [HttpPost]
        public async Task<ActionResult<Motel>> CreateMotel([FromBody] MotelDTO motelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motel = await _motelService.CreateMotelAsync(motelDto);
            return CreatedAtAction(nameof(GetMotelById), new { id = motel.Id }, motel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Motel>> GetMotelById(int id)
        {
            var motel = await _motelService.GetMotelByIdAsync(id);
            if (motel == null)
            {
                return NotFound();
            }
            return Ok(motel);
        }
    }
}