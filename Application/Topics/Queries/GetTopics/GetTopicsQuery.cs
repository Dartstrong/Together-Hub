using Application.Topics.Dtos;

namespace Application.Topics.Queries.GetTopics
{
    public record GetTopicsQuery(CancellationToken CancellationToken) : IQuery<GetTopicsResult>;

    public record GetTopicsResult(List<TopicResponseDto> Topics);
}
