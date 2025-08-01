using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Topics
{
    public class TopicsService(IApplicationDbContext dbContext,
        ILogger<TopicsService> logger) : ITopicsService
    {
        public Task<TopicResponseDto> CreateTopicAsync(Topic topicRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopicAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TopicResponseDto> GetTopicAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var topics = await dbContext.Topics
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return topics.ToTopicResponseDtoList();
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("GetTopicsAsync - the operation was cancelled by the client");
                return [];
            }
            catch (Exception ex)
            {
                logger.LogError($"GetTopicsAsync - operation error: {ex}");
                return [];
            }
            finally
            {
                logger.LogInformation("GetTopicsAsync - operation completed");
            }
        }

        public Task<TopicResponseDto> UpdateTopicAsync(Guid id, Topic topicRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
