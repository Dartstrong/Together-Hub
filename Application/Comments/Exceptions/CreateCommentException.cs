namespace Application.Comments.Exceptions
{
    public class CreateCommentException : CommentException
    {
        public CreateCommentException(Guid id, string text) : base(id, text) { }
    }

}
