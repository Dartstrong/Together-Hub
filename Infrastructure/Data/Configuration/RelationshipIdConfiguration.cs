namespace Infrastructure.Data.Configuration
{
    public class RelationshipIdConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.Property(item => item.Id)
                .HasConversion(
                    id => id.Value,
                    value => RelationshipId.Of(value)
                );
        }
    }
}
