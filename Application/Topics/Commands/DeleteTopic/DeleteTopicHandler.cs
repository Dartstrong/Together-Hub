namespace Application.Topics.Commands.DeleteTopic
{
    public class DeleteTopicHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
    {
        public async Task<DeleteTopicResult> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.Id);

            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if (topic is null || topic.IsDeleted) throw new TopicNotFoundException(request.Id);

            topic.IsDeleted = true;
            topic.DeletedAt = DateTimeOffset.UtcNow;

            var relationships = await dbContext.Relationships
                .Where(r => r.TopicReference == topicId)
                .ToListAsync(cancellationToken);

            foreach (var relationship in relationships)
            {
                relationship.IsDeleted = true;
                relationship.DeletedAt = DateTimeOffset.UtcNow;
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteTopicResult(true);
        }
        
    }
}

