using Application.Dtos.Topics;

namespace Application.Topics.Commands.UpdateTopic
{
    public record UpdateTopicCommand(Guid Id, UpdateTopicDto UpdateTopicDto, CancellationToken CancellationToken) : ICommand<UpdateTopicResult>{}

    public record UpdateTopicResult(TopicResponseDto TopicResponseDto);
}
