using Domain.Enums;

namespace Application.Dtos.Topics
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
