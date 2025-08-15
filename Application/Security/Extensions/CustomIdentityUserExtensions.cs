using Application.Security.Dtos;
using Application.Security.Services;
using Domain.Security;


namespace Application.Security.Extensions
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
