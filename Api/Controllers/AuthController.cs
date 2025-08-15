using Application.Security.Dtos;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new LoginUserQuery(dto, cancellationToken), cancellationToken);
            return Results.Ok(result);
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RegisterUserCommand(dto, cancellationToken), cancellationToken);
            return Results.Created($"/login/", result);
        }
    }
}
