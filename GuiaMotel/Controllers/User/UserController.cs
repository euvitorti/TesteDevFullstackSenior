using DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Repository.Authentication;

namespace GuiaMotel.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                await _authenticationService.RegisterAsync(registerDto);
                return Ok(new { message = "Usuário registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
