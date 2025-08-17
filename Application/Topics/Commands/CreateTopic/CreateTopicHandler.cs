using Application.Security.Services;
using Application.Topics.Extensions;
using AutoMapper;
using Domain.Enums;

namespace Application.Topics.Commands.CreateTopic
{
    public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        : ICommandHandler<CreateTopicCommand, CreateTopicResult>
    {
        public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(
                user => user.UserName == userAccessor.GetUsername()
            );

            var newTopic = mapper.Map<Topic>(request.TopicDto);

            var relationship = Relationship.Create(
                    id: RelationshipId.Of(Guid.NewGuid()),
                    userId: user!.Id,
                    user: user,
                    role: ParticipantRole.Organizer,
                    topicId: newTopic.Id,
                    topic: newTopic
            );


            newTopic.Users.Add(relationship);

            dbContext.Topics.Add(newTopic);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateTopicResult(newTopic.ToTopicResponseDto());
        }
    }
}

