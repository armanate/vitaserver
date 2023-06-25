using Scrypt;

namespace JWTAPI.Security.Hashing;

/// <summary>
/// This password hasher is the same used by ASP.NET Identity.
/// Explanation: https://stackoverflow.com/questions/20621950/asp-net-identity-default-password-hasher-how-does-it-work-and-is-it-secure
/// Full implementation: https://gist.github.com/malkafly/e873228cb9515010bdbe
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        ScryptEncoder encoder = new ScryptEncoder();
        return encoder.Encode(password);

    }

    public bool PasswordMatches(string providedPassword, string passwordHash)
    {
        ScryptEncoder encoder = new ScryptEncoder();
        return encoder.Compare(providedPassword, passwordHash);
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        bool areSame = true;
        for (int i = 0; i < a.Length; i++)
        {
            areSame &= (a[i] == b[i]);
        }
        return areSame;
    }
}