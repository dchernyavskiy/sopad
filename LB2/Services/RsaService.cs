using System.Security.Cryptography;

namespace LB2.Services;

public interface IRsaService
{
    void GenerateKeys();
}

public class RsaService : IRsaService
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    
    public void GenerateKeys()
    {
        using (var rsa = RSA.Create())
        {
            var publicKey = rsa.ExportParameters(false);
            var privateKey = rsa.ExportParameters(true);

            PublicKey = Convert.ToBase64String(publicKey.Modulus);
            PrivateKey = Convert.ToBase64String(privateKey.Modulus);
        }
    }
}