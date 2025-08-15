using Application.Dtos.Topics;

namespace Application.Topics.Commands.CreateTopic
{
    public record CreateTopicCommand(CreateTopicDto TopicDto, CancellationToken CancellationToken) : ICommand<CreateTopicResult>;

    public record CreateTopicResult(TopicResponseDto TopicResponseDto);
}
