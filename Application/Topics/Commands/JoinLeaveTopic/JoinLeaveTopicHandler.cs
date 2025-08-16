using Application.Security.Exceptions;
using Application.Security.Services;
using Domain.Enums;
using Domain.Security;

namespace Application.Topics.Commands.JoinLeaveTopic
{
    public class JoinLeaveTopicHandler(IApplicationDbContext dbContext, IUserAccessor userAccessor)
        : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
    {
        public async Task<JoinLeaveTopicResult> Handle(JoinLeaveTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await GetTopicAsync(request.Id, cancellationToken);
            var currentUser = await GetCurrentUserAsync(cancellationToken);

            var organizer = topic.Users
                .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)?.CurrentUser;

            if (organizer is not null && organizer.UserName == currentUser.UserName)
            {
                return await ToggleTopicStatusAsync(topic, cancellationToken);
            }
            return await UpdateCurrentUserStatusAsync(topic, currentUser, cancellationToken);
        }

        private async Task<Topic> GetTopicAsync(Guid id, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(id);

            var topic = await dbContext.Topics
                .Include(t => t.Users)
                .ThenInclude(r => r.CurrentUser)
                .FirstOrDefaultAsync(t => t.Id == topicId, cancellationToken);

            if (topic is null || topic.IsDeleted) throw new TopicNotFoundException(id);

            return topic;
        }

        private async Task<CustomIdentityUser> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            string username = userAccessor.GetUsername();

            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

            return user is null ? throw new NotValidUsernameException(username) : user;
        }

        private async Task<JoinLeaveTopicResult> ToggleTopicStatusAsync(Topic topic, 
            CancellationToken cancellationToken)
        {
            var oldStatus = topic.IsVoided;
            topic.IsVoided = !oldStatus;

            dbContext.Topics.Update(topic);
            var isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            return new JoinLeaveTopicResult(
                $"Status changed:: {oldStatus} -> {topic.IsVoided}",
                isSuccess
            );
        }

        private async Task<JoinLeaveTopicResult> UpdateCurrentUserStatusAsync(
            Topic topic,
            CustomIdentityUser currentUser,
            CancellationToken cancellationToken)
        {
            var joinUser = topic.Users
                .FirstOrDefault(u => u.CurrentUser.UserName == currentUser.UserName);

            string detail = String.Empty;

            if (joinUser is null)
            {
                var relationship = Relationship.Create(
                    id: RelationshipId.Of(Guid.NewGuid()),
                    userId: currentUser.Id,
                    user: currentUser,
                    role: ParticipantRole.Participant,
                    topicId: topic.Id,
                    topic: topic
                );

                topic.Users.Add(relationship);
                detail = $"You have joined: {topic.Id.Value}";
            }
            else
            {
                topic.Users.Remove(joinUser);
                detail = $"You left: {topic.Id.Value}";
            }

            dbContext.Topics.Update(topic);
            bool isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;
            return new JoinLeaveTopicResult(detail, isSuccess);
        }
    }
}
