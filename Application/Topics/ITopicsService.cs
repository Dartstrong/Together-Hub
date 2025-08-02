namespace Application.Topics
{
   public interface ITopicsService
   {
        Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken cancellationToken);
        Task<TopicResponseDto?> GetTopicAsync(Guid id, CancellationToken cancellationToken);
        Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken);
        Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicDto updateTopictDto, CancellationToken cancellationToken);
        Task DeleteTopicAsync(Guid id, CancellationToken cancellationToken);
   }
}
