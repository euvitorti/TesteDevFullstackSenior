using DTOs.User;

namespace Repository.Authentication
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDTO registerDto);
        Task<string> LoginAsync(LoginDTO loginDto);
    }
}
