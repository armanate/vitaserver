namespace JWTAPI.Persistence;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Connected> Connected { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<Server> Server { get; set; }
    public DbSet<AccountType> AccountType { get; set; }
    public DbSet<BusinessEntityId> BusinessEntityId { get; set; }
    public DbSet<ClientInfo> ClientInfo { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.Entity<User>().HasMany<Payments>(p=>p.Payments).WithOne(p=>p.User).OnDelete(DeleteBehavior.NoAction);
    }
}