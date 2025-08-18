namespace Application.Comments.Exceptions
{
    public class CommentException : Exception
    {
        public CommentException(string message) : base(message) { }

        public CommentException(Guid id, string text)
            : base($"Problem with comment: {text}. TopicId: {{{id}}}")
        {
        }
    }
}
