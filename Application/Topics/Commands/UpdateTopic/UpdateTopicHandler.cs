using Application.Security.Exceptions;
using Application.Security.Services;
using AutoMapper;
using Domain.Enums;

namespace Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicHandler(IApplicationDbContext dbContext, IMapper mapper, 
        IUserAccessor userAccessor)
        : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
    {
        public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            TopicId topicId = TopicId.Of(request.Id);

            var topic = await dbContext.Topics
                .Include(t => t.Users)
                .ThenInclude(r => r.CurrentUser)
                .FirstOrDefaultAsync(t => t.Id == topicId, cancellationToken);

            if (topic is null || topic.IsDeleted) throw new TopicNotFoundException(request.Id);

            string username = userAccessor.GetUsername();
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken)
                ?? throw new NotValidUsernameException(username);

            string organizerUsername = topic.Users
                .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)?
                .CurrentUser?.UserName!;

            if(organizerUsername != username) throw new NotValidOrganizerException(username);

            mapper.Map(request.UpdateTopicDto, topic);

            await dbContext.SaveChangesAsync(CancellationToken.None);

            return new UpdateTopicResult(topic.ToTopicResponseDto());
        }
    }
}

