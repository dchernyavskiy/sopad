using LB1.Services.Contracts;

namespace LB1.Services;

public class DataProcessor : IDataProcessor
{
    private readonly IKeyGenerator _keyGenerator;
    private readonly IConversionService _conversionService;
    private readonly IBlockProcessor _blockProcessor;

    public DataProcessor(IKeyGenerator keyGenerator, IConversionService conversionService,
        IBlockProcessor blockProcessor)
    {
        _keyGenerator = keyGenerator;
        _conversionService = conversionService;
        _blockProcessor = blockProcessor;
    }


    public string Encrypt(string key, string plaintext)
    {
        var K = _keyGenerator.GenerateKeySchedule((ulong)_conversionService.Hash(key));
        _keyGenerator.CheckForWeakKeys(K);
        string binPlaintext = _conversionService.UtfToBin(plaintext);
        int remainder = binPlaintext.Length % 64;
        if (remainder != 0)
        {
            binPlaintext = binPlaintext.PadLeft(64, '0');
        }

        int blockCount = binPlaintext.Length / 64;
        string[] binPlaintextBlocks = new string[blockCount];
        int offset = 0;
        for (int i = 0; i < blockCount; i++)
        {
            binPlaintextBlocks[i] = binPlaintext.Substring(offset, 64);
            offset += 64;
        }

        string[] binCiphertextBlocks = new string[blockCount];

        for (int i = 0; i < blockCount; i++)
        {
            binCiphertextBlocks[i] = _blockProcessor.EncryptBlock(binPlaintextBlocks[i], K);
        }

        string binCiphertext = string.Join(string.Empty, binCiphertextBlocks);

        // Clear the key to ensure it is not stored
        for (int i = 0; i < 16; i++)
        {
            K[i] = 0;
        }

        return binCiphertext;
    }

    public string Decrypt(string key, string plaintext)
    {
        var K = _keyGenerator.GenerateKeySchedule((ulong)_conversionService.Hash(key));
        _keyGenerator.CheckForWeakKeys(K);
        string binPlaintext = plaintext;

        int remainder = binPlaintext.Length % 64;
        if (remainder != 0)
        {
            binPlaintext = binPlaintext.PadLeft(64, '0');
        }

        int blockCount = binPlaintext.Length / 64;
        string[] binPlaintextBlocks = new string[blockCount];
        int offset = 0;
        for (int i = 0; i < blockCount; i++)
        {
            binPlaintextBlocks[i] = binPlaintext.Substring(offset, 64);
            offset += 64;
        }

        string[] binCiphertextBlocks = new string[blockCount];

        for (int i = 0; i < blockCount; i++)
        {
            binCiphertextBlocks[i] = _blockProcessor.DecryptBlock(binPlaintextBlocks[i], K);
        }

        string binCiphertext = string.Join(string.Empty, binCiphertextBlocks);

        // Clear the key to ensure it is not stored
        for (int i = 0; i < 16; i++)
        {
            K[i] = 0;
        }

        return binCiphertext;
    }
}