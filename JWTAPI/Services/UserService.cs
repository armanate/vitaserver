using JWTAPI.Core.Models;

namespace JWTAPI.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<bool> ChekingAnswerAsync(ForgetPassword model)
    {
        try
        {
            var user = await FindByUserNameAsync(model.Username);
            if(user != null)
            {
                if(user.RecoveryAnswer == model.RecoveryAnswer && user.RecoveryQuestion == model.RecoveryQuestion)
                    return true;
                else
                    return false;
            }
            return false;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<CreateUserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles)
    {
        var existingUser = await _userRepository.FindByUserNameAsync(user.Username);

        if (existingUser != null)
        {
            return new CreateUserResponse(false, "Email already in use.", null);
        }

        user.Password = _passwordHasher.HashPassword(user.Password);

        await _userRepository.AddAsync(user, userRoles);
        await _unitOfWork.CompleteAsync();

        return new CreateUserResponse(true, null, user);
    }

    public async Task<CreateUserResponse> CreateUserByPhone(string username,string phone, params ApplicationRole[] userRoles)
    {
        var existingUser = await _userRepository.FindByPhone(phone);
        
        if(existingUser != null)
        {
            return new CreateUserResponse(false, "This phone already in use.", null);
        }
        var user = new User();
        user.Phone = phone;
        user.Username = username;
        user.DateCreated = DateTime.UtcNow;

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return new CreateUserResponse(true, null, user);

    }

    public async Task<User> FindByUserNameAsync(string username)
    {
        return await _userRepository.FindByUserNameAsync(username);
    }

    public async Task<User> FindByPhone(string phone)
    {
        return await _userRepository.FindByPhone(phone);
    }

    public async Task<bool> UpdatePassword(ForgetPassword model)
    {
        bool cheking = await ChekingAnswerAsync(model);
        if (cheking)
        {
            var user = await FindByUserNameAsync(model.Username);
            user.Password = _passwordHasher.HashPassword(model.NewPassword);
            await _userRepository.UpdatePassAsync(user);
            return true;

        }else
        {
            return false;
        }
    }

    public async Task<bool> UpdateUser(User model)
    {
       await _userRepository.UpdatePassAsync(model);
       return true;
    }
}