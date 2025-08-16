﻿using Application.Dtos.Topics;

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
                Users: topic.Users
                );
        }

        public static List<TopicResponseDto> ToTopicResponseDtoList(
            this List<Topic> topics)
        {
            return [.. topics.Select(t => t.ToTopicResponseDto())];
        }
    }
}
