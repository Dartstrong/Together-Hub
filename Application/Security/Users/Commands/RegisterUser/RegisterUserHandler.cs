using Application.Security.Extensions;
using Application.Security.Services;
using AutoMapper;
using Domain.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Users.Commands.RegisterUser
{
    public class RegisterUserHandler(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurityService,
        IMapper mapper)
        : ICommandHandler<RegisterUserCommand, RegisterUserResult>
    {
        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (await manager.Users.AnyAsync(u => u.UserName == request.RegisterUserRequestDto.Username, cancellationToken: cancellationToken)) throw new Exception("Email is busy");

            if (await manager.Users.AnyAsync(u => u.Email == request.RegisterUserRequestDto.Email, cancellationToken: cancellationToken)) throw new Exception("Email is busy");

            var user = mapper.Map<CustomIdentityUser>(request.RegisterUserRequestDto);

            var result = await manager.CreateAsync(user, request.RegisterUserRequestDto.Password!);

            return result.Succeeded ? new RegisterUserResult (user.ToIdentityUserResponseDto(jwtSecurityService)) : (RegisterUserResult)Results.BadRequest(result.Errors);
        }
    }
}
