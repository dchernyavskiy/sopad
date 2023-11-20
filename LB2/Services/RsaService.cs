// using System.Security.Cryptography;
//
// namespace LB2.Services;
//
// public interface IRsaService
// {
//     string PublicKey { get; }
//     string PrivateKey { get; }
//     void GenerateKeys();
// }
//
// public class RsaService : IRsaService
// {
//     public string PublicKey { get; private set; }
//     public string PrivateKey { get; private set; }
//
//     public void GenerateKeys()
//     {
//         using var rsa = new RSACryptoServiceProvider(2048);
//         PublicKey = rsa.ExportSubjectPublicKeyInfoPem();
//         PrivateKey = rsa.ExportSubjectPublicKeyInfoPem();
//     }
// }