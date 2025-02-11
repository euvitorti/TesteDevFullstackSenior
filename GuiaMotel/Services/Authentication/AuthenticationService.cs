using DTOs.User;
using GuiaMotel.Data;
using GuiaMotel.Model;
using Microsoft.EntityFrameworkCore;

namespace Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticationService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="registerDto">Dados de registro do usuário.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns></returns>
        public async Task RegisterAsync(RegisterDTO registerDto, CancellationToken cancellationToken = default)
        {
            // Verifica se já existe um usuário com o mesmo UserName utilizando uma operação assíncrona.
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName, cancellationToken))
                throw new Exception("Usuário já existe.");

            // Cria um novo objeto de usuário com os dados fornecidos.
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                // Criptografa a senha utilizando BCrypt para armazená-la de forma segura.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            // Adiciona o novo usuário ao contexto do Entity Framework.
            _context.Users.Add(user);
            // Persiste as alterações no banco de dados.
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT caso as credenciais sejam válidas.
        /// </summary>
        /// <param name="loginDto">Dados de login do usuário.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns>Token JWT se a autenticação for bem-sucedida.</returns>
        public async Task<string> LoginAsync(LoginDTO loginDto, CancellationToken cancellationToken = default)
        {
            // Busca o usuário pelo UserName de forma assíncrona.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName, cancellationToken);
            
            // Verifica se o usuário foi encontrado e se a senha informada corresponde à senha armazenada.
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            // Gera e retorna o token JWT utilizando o serviço de token.
            return _tokenService.GenerateToken(user);
        }
    }
}
