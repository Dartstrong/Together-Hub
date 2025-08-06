using Application.Security.Dtos;

namespace Application.Security.Users.Queries.LoginUser
{
    public record LoginUserQuery(LoginRequestDto LoginRequestDto, CancellationToken CancellationToken) : IQuery<LoginUserResult>;

    public record LoginUserResult(IdentityUserResponseDto IdentityUserResponseDto);
}
