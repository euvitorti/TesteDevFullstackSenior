using GuiaMotel.Model;

namespace Services.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}