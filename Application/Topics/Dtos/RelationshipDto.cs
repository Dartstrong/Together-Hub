using Domain.Enums;

namespace Application.Topics.Dtos
{
    public record RelationshipDto(
        RelationshipId Id,
        TopicId TopicReference,
        string UserReference,
        ParticipantRole Role,
        TopicResponseDto TopicDto,
        UserProfileDto UserDto
    );
}
