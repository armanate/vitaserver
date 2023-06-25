using JWTAPI.Sms;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace JWTAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenHandler _tokenHandler;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IppanelSms _sms;
    private readonly IMemoryCache _memoryCache;
    public AuthenticationService(
        IOptions<IppanelSms> sms,
        IUserService userService,
        IPasswordHasher passwordHasher,
        ITokenHandler tokenHandler,
        IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
    {
        _tokenHandler = tokenHandler;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _sms = sms.Value;
    }

   
    public async Task<TokenResponse> CreateAccessTokenAsync(string username, string password)
    {
        var user = await _userService.FindByUserNameAsync(username);


        if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
        {
            return new TokenResponse(false, "Invalid credentials.", null);
        }

        var token = _tokenHandler.CreateAccessToken(user);

        return new TokenResponse(true, null, token);
    }

    public async Task<TokenResponse> CreateAccessTokenByPhone(string phone)
    {
        var user = await _userService.FindByPhone(phone);

        if(user == null)
        {
            return new TokenResponse(false, "No user found with this phone", null);
        }

        var token = _tokenHandler.CreateTokenWithPhone(phone);
        user.currentToken = token.Token;
        await _userService.UpdateUser(user);
        return new TokenResponse(true, "Authentication succeed.", token);
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string username)
    {
        var token = _tokenHandler.TakeRefreshToken(refreshToken, username);

        if (token == null)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        if (token.IsExpired())
        {
            return new TokenResponse(false, "Expired refresh token.", null);
        }

        var user = await _userService.FindByUserNameAsync(username);
        if (user == null)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        var accessToken = _tokenHandler.CreateAccessToken(user);
        return new TokenResponse(true, null, accessToken);
    }

    public void RevokeRefreshToken(string refreshToken, string username)
    {
        _tokenHandler.RevokeRefreshToken(refreshToken, username);
    }
    // 0 in respons is error in request
    // 1 in respons you are get befor and not use otp 
    public async Task<string> SendSmsOtp(string phonnumber)
    {
        Random rnd = new Random();
        int num = rnd.Next(10000, 99999);
        string url = $"http://{_sms.Url}/?apikey={_sms.Apikey}=&pid={_sms.Pid}&fnum={_sms.SenderNumber}&tnum={phonnumber}&p1=code&v1={num}";
        int value = 0;
        _memoryCache.TryGetValue(phonnumber, out value);
        if(value >0)
        {
            return "1";
        }
        var client = _httpClientFactory.CreateClient();
        var respons = await client.GetAsync(url);
        if (respons.IsSuccessStatusCode)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(_sms.AbsoluteExpiration),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(_sms.AbsoluteExpiration)
            };
            _memoryCache.Set(phonnumber, num, cacheExpiryOptions);
          
            return await respons.Content.ReadAsStringAsync();
        }
        return "0";
    }
    public async Task<bool> ChekingOtp(string phonnumber, int otp)
    {
        int value = 0;
        _memoryCache.TryGetValue(phonnumber, out value);
        if (value > 0 && value == otp)
        {
            _memoryCache.Remove(phonnumber);
            return true;
        }
        else
        {
            return false;
        }
    }

}