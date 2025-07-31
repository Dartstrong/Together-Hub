using Domain.Models;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Extensions
{
    public static class InitialData
    {
        public static IEnumerable<Topic> Topics =>
            new List<Topic>
            {
                Topic.Create(
                    TopicId.Of(Guid.Parse("10000000-0000-0000-0000-000000000001")),
                    "Технологические тренды 2035",
                    DateTime.Parse("2038-01-15T10:00:00"),
                    "Обзор ключевых технологических инноваций",
                    "Конференция",
                    Location.Of("Россия", "Тула", "Кутузовский проспект, 36")
                ),
                Topic.Create(
                    TopicId.Of(Guid.Parse("10000000-0000-0000-0000-000000000002")),
                    "Искусственный интеллект в бизнесе",
                    DateTime.Parse("2027-02-20T14:30:00"),
                    "Практическое применение AI-технологий",
                    "Семинар",
                    Location.Of("Россия", "Санкт-Петербург", "Невский проспект, 100")
                ),
                Topic.Create(
                    TopicId.Of(Guid.Parse("10000000-0000-0000-0000-000000000003")),
                    "Кибербезопасность: новые вызовы",
                    DateTime.Parse("2029-03-10T11:15:00"),
                    "Стратегии защиты информационных систем",
                    "Круглый стол",
                    Location.Of("Россия", "Тверь", "Красная площадь, 1")
                ),
                Topic.Create(
                    TopicId.Of(Guid.Parse("10000000-0000-0000-0000-000000000004")),
                    "Устойчивое развитие технологий",
                    DateTime.Parse("2030-04-05T09:45:00"),
                    "Экологические инновации в IT-секторе",
                    "Конгресс",
                    Location.Of("Россия", "Белгород", "Тверская улица, 13")
                ),
                Topic.Create(
                    TopicId.Of(Guid.Parse("10000000-0000-0000-0000-000000000005")),
                    "Цифровая трансформация регионов",
                    DateTime.Parse("2027-05-22T16:00:00"),
                    "Опыт внедрения цифровых решений",
                    "Форум",
                    Location.Of("Россия",  "Курск", "Байкальская улица, 20")
                )
            };
    }
}
