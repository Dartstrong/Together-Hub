namespace Application.Topics.Queries.GetTopic
{
    public class GetTopicHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetTopicQuery, GetTopicResult>
    {
        public async Task<GetTopicResult> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.Id);

            var topic = await dbContext.Topics
                .Include(t => t.Users)
                .ThenInclude(r => r.CurrentUser)
                .FirstOrDefaultAsync(t =>t.Id == topicId, cancellationToken);

            return new GetTopicResult(topic == null || topic.IsDeleted ? throw new TopicNotFoundException(request.Id) : topic.ToTopicResponseDto());
        }
    }
}
