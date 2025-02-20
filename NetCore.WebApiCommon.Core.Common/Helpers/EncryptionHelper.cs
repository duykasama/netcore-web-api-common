using Microsoft.AspNetCore.Identity;

namespace NetCore.WebApiCommon.Core.Common.Helpers;

public static class EncryptionHelper
{
    public static string Encrypt(string rawValue)
    {
        PasswordHasher<string> hasher = new();
        return hasher.HashPassword(default!, rawValue);
    }

    public static bool Verify(string rawValue, string encryptedValue)
    {
        PasswordHasher<string> hasher = new();
        PasswordVerificationResult verficationResult = hasher.VerifyHashedPassword(default!, encryptedValue, rawValue);
        return verficationResult == PasswordVerificationResult.Success;
    }
}