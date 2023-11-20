using System.Security.Cryptography;
using System.Text;
using LB2.Models.Responses;
using LB2.Services.Contracts;
using Microsoft.Extensions.Options;
using RSAExtensions;

namespace LB2.Services;

public class AuthService : IAuthService
{
    private readonly RsaOptions _rsaOptions;

    public AuthService(IOptions<RsaOptions> rsaOptions)
    {
        _rsaOptions = rsaOptions.Value;
    }

    private string Decrypt(string encryptedData)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        using var rsa = new RSACryptoServiceProvider(2048);
        rsa.ImportFromPem(_rsaOptions.PrivateKey);
        byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);
        return Encoding.ASCII.GetString(decryptedBytes);
    }

    public LoginResponse Login(string encryptedLogin, string encryptedPassword, string publicKey)
    {
        var login = Decrypt(encryptedLogin);
        var password = Decrypt(encryptedPassword);
        using var rsa2 = RSA.Create();
        rsa2.ImportFromPem(publicKey);
        var eLogin = Convert.ToBase64String(rsa2.Encrypt(Encoding.ASCII.GetBytes(login), RSAEncryptionPadding.Pkcs1));
        var ePassword =
            Convert.ToBase64String(rsa2.Encrypt(Encoding.ASCII.GetBytes(password), RSAEncryptionPadding.Pkcs1));
        return new LoginResponse(eLogin, ePassword);
    }
}