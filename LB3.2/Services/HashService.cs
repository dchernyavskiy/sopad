using System.Text;
using Aspose.Words;

namespace LB3.Services;

public interface IHashService
{
    byte Hash(string data, int bitSize);
    byte Hash(byte[] data, int bitSize);
    byte[] Collision(byte[] data, int bitSize);
    byte[] CollisionInWord(Document data, int bitSize);
    byte[] CollisionInTheMiddle(byte[] data, int bitSize);
}

public class HashService : IHashService
{
    public byte Hash(string data, int bitSize)
    {
        return Hash(Encoding.UTF8.GetBytes(data), bitSize);
    }

    public byte Hash(byte[] data, int bitSize)
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

    public byte[] Collision(byte[] data, int bitSize)
    {
        var initHash = Hash(data, bitSize);
        var mutated = MutateByteArray(data, 1);
        var mutatedHash = Hash(mutated, bitSize);
        while (initHash != mutatedHash)
        {
            mutated = MutateByteArray(data, 1);
            mutatedHash = Hash(mutated, bitSize);
        }

        return mutated;
    }

    public byte[] CollisionInWord(Document data, int bitSize)
    {
        using var ms = new MemoryStream();
        data.Save(ms, SaveFormat.Docx);
        var initHash = Hash(ms.ToArray(), bitSize);

        var text = data.GetText();
        var bytes = Encoding.ASCII.GetBytes(text);
        var mutatedBytes = MutateByteArray(bytes, 1);
        var encoded = Encoding.ASCII.GetString(mutatedBytes);
        var builder = new DocumentBuilder(data);
        builder.MoveToDocumentStart();
        builder.Write(encoded);
        data.Save(ms, SaveFormat.Docx);
        var newHash = Hash(ms.ToArray(), bitSize);

        while (initHash != newHash)
        {
            text = data.GetText();
            bytes = Encoding.ASCII.GetBytes(text);
            mutatedBytes = MutateByteArray(bytes, 1);
            encoded = Encoding.ASCII.GetString(mutatedBytes);
            builder = new DocumentBuilder(data);
            builder.MoveToDocumentStart();
            builder.Write(encoded);
            data.Save(ms, SaveFormat.Docx);
            newHash = Hash(ms.ToArray(), bitSize);
        }

        return ms.ToArray();
    }

    public byte[] CollisionInTheMiddle(byte[] data, int bitSize)
    {
        var initHash = Hash(data, bitSize);
        var random = new Random();
        var middle = data.Length / 2;
        data[random.Next(middle - 5, middle + 5)] ^= (byte)(1 << random.Next(8));
        var mutatedHash = Hash(data, bitSize);
        while (initHash != mutatedHash)
        {
            data[random.Next(middle - 5, middle + 5)] ^= (byte)(1 << random.Next(8));
            mutatedHash = Hash(data, bitSize);
        }

        return data;
    }

    private byte[] MutateByteArray(byte[] original, double mutationProbability)
    {
        if (original == null)
        {
            throw new ArgumentNullException(nameof(original));
        }

        if (mutationProbability < 0 || mutationProbability > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(mutationProbability),
                "Mutation probability must be between 0 and 1.");
        }

        byte[] mutated = new byte[original.Length];
        original.CopyTo(mutated, 0);
        var random = new Random();
        for (int i = 0; i < mutated.Length; i++)
        {
            if (random.NextDouble() < mutationProbability)
            {
                mutated[i] ^= (byte)(1 << random.Next(8));
            }
        }

        return mutated;
    }
}