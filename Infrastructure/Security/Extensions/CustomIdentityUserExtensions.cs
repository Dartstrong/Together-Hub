using Application.Security.Dtos;
using Domain.Security;
using Infrastructure.Security.Services;

namespace Infrastructure.Security.Extensions
{
    public static class CustomIdentityUserExtensions
    {
        public static IdentityUserResponseDto ToIdentityUserResponseDto(
            this CustomIdentityUser user, IJwtSecurityService jwtSecurityService)
        {
            return new IdentityUserResponseDto(
                    user.UserName!,
                    user.Email!,
                    jwtSecurityService.CreateToken(user));
        }
    }
}
