namespace Application.Security.Dtos
{
    public record RegisterUserRequestDto
    (
        string FullName,
        string Username,
        string Email,
        string Password
    );
}
