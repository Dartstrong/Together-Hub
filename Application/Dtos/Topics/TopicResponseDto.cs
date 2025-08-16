namespace Application.Dtos.Topics
{
    public record TopicResponseDto (
        Guid Id,
        string Title,
        string Summary,
        string TopicType,
        LocationDto Location,
        DateTime? EventStart,
        List<UserProfileDto> Users
    );
}
