using GuiaMotel.Model;

namespace Repository.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}