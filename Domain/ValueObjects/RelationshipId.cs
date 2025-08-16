namespace Domain.ValueObjects
{
    public record RelationshipId
    {
        public Guid Value { get; }
        private RelationshipId(Guid value) => Value = value;

        public static RelationshipId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("RelationshipId can't be empty");
            }
            return new RelationshipId(value);
        }
    }
}
