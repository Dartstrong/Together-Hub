using Application.Topics.Dtos;
using Application.Topics.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Topics.Old
{
    public class TopicsService(IApplicationDbContext dbContext,
        ILogger<TopicsService> logger) : ITopicsService
    {
        public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken)
        {
            Topic topic = Topic.Create(
                TopicId.Of(Guid.NewGuid()),
                createTopicDto.Title,
                createTopicDto.EventStart,
                createTopicDto.Summary,
                createTopicDto.TopicType,
                Location.Of(createTopicDto.Location.Country, 
                    createTopicDto.Location.City,
                    createTopicDto.Location.Street)
                );
            
            dbContext.Topics.Add(topic);

            await dbContext.SaveChangesAsync(cancellationToken);

            return topic.ToTopicResponseDto();
        }

        public async Task DeleteTopicAsync(Guid id, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(id);

            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if (topic is null || topic.IsDeleted) throw new TopicNotFoundException(id);

            topic.IsDeleted  = true;
            topic.DeletedAt = DateTimeOffset.UtcNow;

            //dbContext.Topics.Remove(topic);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TopicResponseDto?> GetTopicAsync(Guid id, CancellationToken cancellationToken)
        {
             TopicId topicId = TopicId.Of(id);

             var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

             return topic == null || topic.IsDeleted ? throw new TopicNotFoundException(id) : (topic?.ToTopicResponseDto());       
        }

        public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken cancellationToken)
        {
            var topics = await dbContext.Topics
                    .AsNoTracking()
                    .Where(t => !t.IsDeleted)
                    .ToListAsync(cancellationToken);

            return topics.ToTopicResponseDtoList();
        }

        public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicDto updateTopictDto, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(id);

            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if(topic is null || topic.IsDeleted) throw new TopicNotFoundException(id);

            topic.Title = updateTopictDto.Title ?? topic.Title;
            topic.Summary = updateTopictDto.Summary ?? topic.Summary;
            topic.TopicType = updateTopictDto.TopicType ?? topic.TopicType;
            topic.EventStart = updateTopictDto.EventStart;
            topic.Location = Location.Of(
                updateTopictDto.Location.Country,
                updateTopictDto.Location.City,
                updateTopictDto.Location.Street
            );

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return topic.ToTopicResponseDto();
        }
    }
}
