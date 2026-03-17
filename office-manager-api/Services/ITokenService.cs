using office_manager_api.Models;

namespace office_manager_api.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
