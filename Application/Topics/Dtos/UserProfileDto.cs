namespace Application.Topics.Dtos
{
    public record UserProfileDto(
        string Id,
        string Username,
        string FullName,
        string Role
    );
}
