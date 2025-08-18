using Application.Comments.Dtos;
using Application.Comments.Exceptions;
using Application.Security.Exceptions;
using Application.Security.Services;
using AutoMapper;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentHandler(IApplicationDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        : ICommandHandler<CreateCommentCommand, CreateCommentResult>
    {
        public async Task<CreateCommentResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.TopicId);
            var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

            if (topic is null || topic.IsDeleted) throw new TopicNotFoundException(request.TopicId);

            string username = userAccessor.GetUsername();

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username, cancellationToken)
                ?? throw new NotValidUsernameException(username);

            var comment = Comment.Create(
                id: CommentId.Of(Guid.NewGuid()),
                text: request.Body,
                topic: topic,
                author: user
            );

            topic.Comments.Add(comment);
            var success = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            if (success)
            {
                var result = mapper.Map<CommentDto>(comment);
                return new CreateCommentResult(result);
            }

            throw new CreateCommentException(topic.Id.Value, request.Body);
        }
    }
}
