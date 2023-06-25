namespace JWTAPI.Core.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user, ApplicationRole[] userRoles);
    Task AddAsync(User user);

    Task<User> FindByUserNameAsync(string username);
    Task UpdatePassAsync(User user);

    Task<User> FindByPhone(string phone);

}