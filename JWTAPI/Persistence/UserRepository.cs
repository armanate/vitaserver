namespace JWTAPI.Persistence;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, ApplicationRole[] userRoles)
    {
        var roleNames = userRoles.Select(r => r.ToString()).ToList();
        var roles = await _context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

        foreach (var role in roles)
        {
            user.UserRoles.Add(new UserRole { RoleId = role.Id });
        }

        _context.Users.Add(user);
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User> FindByPhone(string phone)
    {
        return await _context.Users.Include(c => c.Connected)
            .Include(c => c.UserRoles).Include(c => c.Payments.Where(i => i.ExpireDate > DateTime.UtcNow))
            .FirstOrDefaultAsync(_ => _.Phone.Equals(phone));
    }

    public async Task<User> FindByUserNameAsync(string username)
    {
        return await _context.Users
            .Include(_ => _.UserRoles)
                .ThenInclude(_ => _.Role)
            .FirstOrDefaultAsync(_ => _.Username.Equals(username));

    }

    public async Task UpdatePassAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}