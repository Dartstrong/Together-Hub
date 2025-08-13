using Application.Security.Dtos;
using Infrastructure.Security.Extensions;
using Infrastructure.Security.Services;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator, IJwtSecurityService jwtSecurityService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new LoginUserQuery(dto, cancellationToken), cancellationToken);
            return Results.Ok(result.CustomIdentityUser.ToIdentityUserResponseDto(jwtSecurityService));
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RegisterUserCommand(dto, cancellationToken), cancellationToken);
            return Results.Created($"/login/", result.CustomIdentityUser.ToIdentityUserResponseDto(jwtSecurityService));
        }
    }

}
