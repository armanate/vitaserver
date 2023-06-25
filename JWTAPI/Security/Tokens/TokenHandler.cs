using JWTAPI.Core.Models;
using JWTAPI.Core.Security.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace JWTAPI.Security.Tokens;

public class TokenHandler : ITokenHandler
{
    private readonly ISet<RefreshTokenWithEmail> _refreshTokens = new HashSet<RefreshTokenWithEmail>();

    private readonly TokenOptions _tokenOptions;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly IPasswordHasher _passwordHaser;

    public TokenHandler(
        IOptions<TokenOptions> tokenOptionsSnapshot,
        SigningConfigurations signingConfigurations,
        IPasswordHasher passwordHaser)
    {
        _passwordHaser = passwordHaser;
        _tokenOptions = tokenOptionsSnapshot.Value;
        _signingConfigurations = signingConfigurations;
    }

    public AccessToken CreateAccessToken(User user)
    {
        var refreshToken = BuildRefreshToken();
        var accessToken = BuildAccessToken(user, refreshToken);

        _refreshTokens.Add(new RefreshTokenWithEmail
        {
            Email = user.Email,
            RefreshToken = refreshToken,
        });

        return accessToken;
    }

    public AccessToken CreateTokenWithPhone(string phone)
    {
        var refreshToken = BuildRefreshToken();

        var accessTokenExpiration = DateTime.UtcNow.AddYears(10);

        var securityToken = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: GetClaimsByPhone(phone),
            notBefore: DateTime.UtcNow,
            expires: accessTokenExpiration,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
    }

    public RefreshToken TakeRefreshToken(string token, string userEmail)
    {
        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userEmail))
        {
            return null;
        }

        var refreshTokenWithEmail = _refreshTokens.SingleOrDefault(t => t.RefreshToken.Token == token && t.Email == userEmail);

        if (refreshTokenWithEmail == null)
        {
            return null;
        }

        _refreshTokens.Remove(refreshTokenWithEmail);

        return refreshTokenWithEmail.RefreshToken;
    }

    public void RevokeRefreshToken(string token, string userEmail)
    {
        TakeRefreshToken(token, userEmail);
    }

    public String ValidateToken(String token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                IssuerSigningKey = _signingConfigurations.SecurityKey,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var phone = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // return phone from JWT token if validation successful
            return phone;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    private RefreshToken BuildRefreshToken()
    {
        var refreshToken = new RefreshToken
        (
            token: _passwordHaser.HashPassword(Guid.NewGuid().ToString()),
            expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
        );

        return refreshToken;
    }

    private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: GetClaims(user),
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email)
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }

        return claims;
    }

    private IEnumerable<Claim> GetClaimsByPhone(string phone)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, phone)
        };

        return claims;
    }
}
