using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    public static string GenerateSalt(int size = 32)
    {
        var rng = new RNGCryptoServiceProvider();
        var buffer = new byte[size];
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }

    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = password + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
