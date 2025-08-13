using Domain.Security;

namespace Infrastructure.Security.Services
{
    public interface IJwtSecurityService
    {
        string CreateToken(CustomIdentityUser user);
    }
}
