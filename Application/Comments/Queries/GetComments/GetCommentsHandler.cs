using Application.Comments.Dtos;
using AutoMapper.QueryableExtensions;

namespace Application.Comments.Queries.GetComments
{
    public class GetCommentsHandler(IApplicationDbContext dbContext, IMapper mapper)
        : IQueryHandler<GetCommentsQuery, GetCommentsResult>
    {
        public async Task<GetCommentsResult> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var topicId = TopicId.Of(request.TopicId);

            var comments = await dbContext.Comments
                .Where(c => c.CurrentTopic.Id == topicId)
                .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.CreateAt)
                .ToListAsync(cancellationToken);

            return new GetCommentsResult(comments);
        }
    }
}

