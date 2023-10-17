using System.Security.Cryptography;
using System.Text;
using LB2.Models.Responses;
using LB2.Services.Contracts;
using Microsoft.Extensions.Options;

namespace LB2.Services;

public class AuthService : IAuthService
{
    private readonly RsaOptions _options;

    public AuthService(RsaOptions options)
    {
        _options = options;
    }

    private string Decrypt(string encryptedData)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportFromPem(_options.PrivateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    public LoginResponse Login(string encryptedLogin, string encryptedPassword)
    {
        var login = Decrypt(encryptedLogin);
        var password = Decrypt(encryptedPassword);
        return new LoginResponse(login, password);
    }
}