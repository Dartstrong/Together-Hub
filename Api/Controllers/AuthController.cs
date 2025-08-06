using Application.Security.Dtos;
using Application.Security.Services;
using Application.Security.Users.Commands.RegisterUser;
using Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator, UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurityService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await manager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                return Results.Unauthorized();
            }

            var result = await manager.CheckPasswordAsync(user, dto.Password);

            if (result)
            {
                var response = new IdentityUserResponseDto(
                    user.UserName!,
                    user.Email!,
                    jwtSecurityService.CreateToken(user)
                );
                return Results.Ok(new { result = response });
            }

            return Results.Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequestDto dto, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RegisterUserCommand(dto, cancellationToken), cancellationToken);
            return Results.Created($"/login/", response.IdentityUserResponseDto);
        }
    }

}
