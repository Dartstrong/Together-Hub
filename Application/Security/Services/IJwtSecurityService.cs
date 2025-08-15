using Domain.Security;

namespace Application.Security.Services
{
    public interface IJwtSecurityService
    {
        string CreateToken(CustomIdentityUser user);
    }
}
