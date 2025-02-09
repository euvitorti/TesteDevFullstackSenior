using DTOs.User;
using GuiaMotel.Data;
using GuiaMotel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaMotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDto)
        {
            // Validação automática dos atributos do DTO (ex: [Required], [EmailAddress])
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Users.Any(u => u.UserName == registerDto.UserName))
                return BadRequest("Usuário já existe.");

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email
            }).ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
    }
}
