using Application.Topics.Dtos;

namespace Application.Topics.Queries.GetTopic
{
    public record GetTopicQuery(Guid Id, CancellationToken CancellationToken) : IQuery<GetTopicResult>;

    public record GetTopicResult(TopicResponseDto Topic);
}
