using Application.Security.Exceptions;
using Application.Security.Extensions;
using Application.Security.Services;
using AutoMapper;
using Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Users.Commands.RegisterUser
{
    public class RegisterUserHandler(UserManager<CustomIdentityUser> manager, IMapper mapper, IJwtSecurityService jwtSecurityService)
        : ICommandHandler<RegisterUserCommand, RegisterUserResult>
    {
        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (await manager.Users.AnyAsync(u => u.UserName == request.RegisterUserRequestDto.Username, cancellationToken: cancellationToken)) throw new NotValidUsernameException(request.RegisterUserRequestDto.Username);

            if (await manager.Users.AnyAsync(u => u.Email == request.RegisterUserRequestDto.Email, cancellationToken: cancellationToken)) throw new NotValidEmailException(request.RegisterUserRequestDto.Email);

            var user = mapper.Map<CustomIdentityUser>(request.RegisterUserRequestDto);

            var result = await manager.CreateAsync(user, request.RegisterUserRequestDto.Password!);

            return result.Succeeded ? new RegisterUserResult(user.ToIdentityUserResponseDto(jwtSecurityService)) : throw new Exception(result.Errors.ToString());
        }
    }
}
