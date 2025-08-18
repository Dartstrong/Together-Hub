using Domain.Security;

namespace Application.Data.DataBaseContext
{
    public interface IApplicationDbContext
    {
        DbSet<Topic> Topics { get;}
        DbSet<Relationship> Relationships { get; }
        DbSet<CustomIdentityUser> Users { get; set; }
        DbSet<Comment> Comments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
