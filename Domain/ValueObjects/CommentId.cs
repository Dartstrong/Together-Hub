namespace Domain.ValueObjects
{
    public record CommentId
    {
        public Guid Value { get; }

        private CommentId(Guid value)
        {
            this.Value = value;
        }

        public static CommentId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("CommentId can't be empty");
            }
            return new CommentId(value);
        }
    }
}
