namespace JWTAPI.Core.Services;

public interface IAuthenticationService
{
    Task<TokenResponse> CreateAccessTokenAsync(string username, string password);
    Task<TokenResponse> CreateAccessTokenByPhone(string phone);
    Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
    void RevokeRefreshToken(string refreshToken, string userEmail);
    Task<string> SendSmsOtp(string phonnumber);
    Task<bool> ChekingOtp(string phonnumber,int otp);

}