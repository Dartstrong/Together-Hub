using Application.Security.Exceptions;
using Application.Security.Extensions;
using Application.Security.Services;
using Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Users.Queries.LoginUser
{ 
    public class LoginUserHandler(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurityService)
        : IQueryHandler<LoginUserQuery, LoginUserResult>
    {

        public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await manager.FindByEmailAsync(request.LoginRequestDto.Email) ?? throw new NotValidEmailException(request.LoginRequestDto.Email);

            var result = await manager.CheckPasswordAsync(user, request.LoginRequestDto.Password);

            return result ? new LoginUserResult(user.ToIdentityUserResponseDto(jwtSecurityService)) : throw new NotValidPassException();
        }
    }
}
