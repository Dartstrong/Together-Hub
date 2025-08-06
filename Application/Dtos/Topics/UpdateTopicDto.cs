namespace Application.Dtos.Topics
{
    public record UpdateTopicDto(
        string Title,
        string Summary,
        string TopicType,
        LocationDto Location,
        DateTime EventStart
    );
}
