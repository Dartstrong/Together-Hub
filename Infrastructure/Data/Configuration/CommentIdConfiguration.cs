namespace Infrastructure.Data.Configuration
{
    public class CommentIdConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(comment => comment.Id)
                .HasConversion(
                    id => id.Value,
                    value => CommentId.Of(value)
                );
        }
    }
}
