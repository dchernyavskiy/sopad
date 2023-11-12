using System.Text;

namespace LB5.Services;

public interface IHashService
{
    byte Hash(string data, int bitSize);
}

public class HashService : IHashService
{
    public byte Hash(string data, int bitSize)
    {
        return GetHash(Encoding.UTF8.GetBytes(data), bitSize);
    }

    private byte GetHash(byte[] data, int bitSize)
    {
        if (bitSize != 2 && bitSize != 4 && bitSize != 8)
        {
            throw new ArgumentException("Bit size should be 2, 4, or 8.");
        }

        int hash = 0;
        int mask = (1 << bitSize) - 1;
        int changeThreshold = data.Length * 30 / 100;
        for (int i = 0; i < data.Length; i++)
        {
            hash = (hash + data[i]) & mask;
            if (i >= changeThreshold)
            {
                int change = (data[i] * i) % mask;
                hash = (hash + change) & mask;
            }
        }

        return (byte)hash;
    }
}