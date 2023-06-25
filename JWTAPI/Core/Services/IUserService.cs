namespace JWTAPI.Core.Services;

public interface IUserService
{
     Task<CreateUserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles);
     Task<CreateUserResponse> CreateUserByPhone(string username,string phone, params ApplicationRole[] userRoles);
    Task<User> FindByUserNameAsync(string username);
    Task<User> FindByPhone(string phone);
     Task<bool> ChekingAnswerAsync(ForgetPassword model);
     Task<bool> UpdatePassword(ForgetPassword model);

    Task<bool> UpdateUser(User model);


}