using System.Numerics;

namespace LB4.Services;

public interface IDiffieHellmanService
{
    BigInteger GenerateKeySecret(BigInteger myPrivateKey, BigInteger anotherPublicKey);
}

public class DiffieHellmanService : IDiffieHellmanService
{
    public static readonly int DH_KEY_LENGTH = 16;

    private static readonly BigInteger P = BigInteger.Pow(2, 128) - 159;
    private static readonly BigInteger G = 5;

    public BigInteger GenerateKeySecret(BigInteger myPrivateKey, BigInteger anotherPublicKey)
    {
        /* secret_key = other_key^prv_key mod P*/
        var secretK = BigInteger.ModPow(anotherPublicKey, myPrivateKey, P);
        var secretKey = secretK.ToByteArray();
        Array.Resize(ref secretKey, DH_KEY_LENGTH); // Trim or pad to the required length
        return new BigInteger(secretKey);
    }
}