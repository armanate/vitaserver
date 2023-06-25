namespace JWTAPI.Core.Security.Tokens;

public interface ITokenHandler
{
     AccessToken CreateAccessToken(User user);
     AccessToken CreateTokenWithPhone(string phone);

    String ValidateToken(string token);
     RefreshToken TakeRefreshToken(string token, string username);
     void RevokeRefreshToken(string token, string username);
}