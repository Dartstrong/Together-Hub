namespace Application.Dtos.Topics
{
    public record UserProfileDto(
        string Id,
        string Username,
        string FullName,
        string Role
    );
}
