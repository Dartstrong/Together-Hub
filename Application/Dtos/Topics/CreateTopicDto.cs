namespace Application.Dtos.Topics
{
    public record CreateTopicDto(
        string Title,
        string Summary,
        string TopicType,
        LocationDto Location,
        DateTime? EventStart
    );
}
