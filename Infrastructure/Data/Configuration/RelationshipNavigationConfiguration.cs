namespace Infrastructure.Data.Configuration
{
    public class RelationshipNavigationConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.HasOne(r => r.CurrentTopic)
                .WithMany(t => t.Users)
                .HasForeignKey(r => r.TopicReference);

            builder.HasOne(r => r.CurrentUser)
                .WithMany(t => t.Topics)
                .HasForeignKey(r => r.UserReference);
        }
    }
}
