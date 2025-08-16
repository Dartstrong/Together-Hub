using Domain.Enums;
using Domain.Security;

namespace Domain.Models
{
    public class Relationship : Entity<RelationshipId>
    {
        public required TopicId TopicReference { get; set; }
        public required Topic CurrentTopic { get; set; }

        public required string UserReference { get; set; }
        public required CustomIdentityUser CurrentUser { get; set; }

        public ParticipantRole Role { get; set; }

        public static Relationship Create(
            RelationshipId id,
            string userId,
            CustomIdentityUser user,
            ParticipantRole role,
            TopicId topicID,
            Topic topic)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userId);

            return new Relationship
            {
                Id = id,
                TopicReference = topicID,
                CurrentTopic = topic,
                UserReference = userId,
                CurrentUser = user,
                Role = role
            };
        }
    }
}
