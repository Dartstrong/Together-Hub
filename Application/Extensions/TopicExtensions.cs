using Application.Dtos.Topics;

namespace Application.Extensions
{
    public static class TopicExtensions
    {
        public static TopicResponseDto ToTopicResponseDto(
            this Topic topic)
        {
            return new TopicResponseDto(
                Id: topic.Id.Value,
                Title: topic.Title,
                Summary: topic.Summary,
                TopicType: topic.TopicType,
                Location: new LocationDto(
                    topic.Location.Country,
                    topic.Location.City,
                    topic.Location.Street),
                EventStart: topic.EventStart,
                IsVoided : topic.IsVoided,
                Users: [.. topic.Users.Select(r => new UserProfileDto
                (
                    r.CurrentUser.Id,
                    r.CurrentUser.UserName!,
                    r.CurrentUser.FullName,
                    r.Role.ToString()
                ))]
            );
        }

        public static List<TopicResponseDto> ToTopicResponseDtoList(
            this List<Topic> topics)
        {
            return [.. topics.Select(t => t.ToTopicResponseDto())];
        }
    }
}
