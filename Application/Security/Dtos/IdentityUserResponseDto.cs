namespace Application.Security.Dtos
{
    public record IdentityUserResponseDto
    (       
        string Username,   
        string Email,
        string JwtToken
    );
}
