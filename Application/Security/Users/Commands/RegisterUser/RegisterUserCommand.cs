using Application.Security.Dtos;
using Domain.Security;

namespace Application.Security.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(RegisterUserRequestDto RegisterUserRequestDto, CancellationToken CancellationToken) : ICommand<RegisterUserResult>;

    public record RegisterUserResult(IdentityUserResponseDto IdentityUserResponseDto);
}
