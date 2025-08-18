namespace Application.Comments.Dtos
{
    public class CommentDto
    {
        public string Id { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateTime CreateAt { get; set; }
    }
}
