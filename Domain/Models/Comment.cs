using Domain.Security;

namespace Domain.Models
{
    public class Comment : Entity<CommentId>
    {
        public required CustomIdentityUser Author { get; set; }
        public Topic CurrentTopic { get; set; } = default!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string Text { get; set; } = default!;

        public static Comment Create(CommentId id, string text, CustomIdentityUser author, Topic topic)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(text);

            return new Comment
            {
                Id = id,
                Author = author,
                CurrentTopic = topic,
                Text = text
            };
        }
    }
}
