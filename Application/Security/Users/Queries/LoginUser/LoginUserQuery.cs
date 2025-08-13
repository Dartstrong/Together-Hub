using Application.Security.Dtos;
using Domain.Security;

namespace Application.Security.Users.Queries.LoginUser
{
    public record LoginUserQuery(LoginRequestDto LoginRequestDto, CancellationToken CancellationToken) : IQuery<LoginUserResult>;

    public record LoginUserResult(CustomIdentityUser CustomIdentityUser);
}
