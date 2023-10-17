using System.Text;
using LB1.Services.Contracts;

namespace LB1.Services;

public class ConversionService : IConversionService
{
    public long Hash(string input)
    {
        const long prime = 1125899906842597;
        long h = prime;
        int length = input.Length;

        for (int i = 0; i < length; i++)
        {
            h = 31 * h + input[i];
        }

        return h;
    }

    public string BinToHex(string binary)
    {
        byte[] binBytes = new byte[binary.Length / 8];
        for (int i = 0; i < binary.Length; i += 8)
        {
            string byteStr = binary.Substring(i, 8);
            long val = Convert.ToInt64(byteStr, 2);
            binBytes[i / 8] = (byte)val;
        }

        string hexStr = BitConverter.ToString(binBytes).Replace("-", "");
        return hexStr;
    }

    public string HexToBinary(string hexStr)
    {
        byte[] hexBytes = HexToBytes(hexStr);
        StringBuilder binStr = new StringBuilder();

        foreach (byte b in hexBytes)
        {
            binStr.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
        }

        return binStr.ToString();
    }

    private byte[] HexToBytes(string hexStr)
    {
        int numberChars = hexStr.Length;
        byte[] bytes = new byte[numberChars / 2];

        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexStr.Substring(i, 2), 16);
        }

        return bytes;
    }

    public string UtfToBin(string utfStr)
    {
        byte[] utfBytes = Encoding.UTF8.GetBytes(utfStr);
        string binStr = "";

        foreach (byte b in utfBytes)
        {
            binStr += Convert.ToString(b, 2).PadLeft(8, '0');
        }

        return binStr;
    }

    public string BinToUTF(string input)
    {
        string output = string.Empty;

        while (input.Length > 0)
        {
            string charBinary = input.Substring(0, 8);
            input = input.Substring(8);
            int a = 0;
            int degree = charBinary.Length - 1;

            foreach (char c in charBinary)
            {
                int digit = c - '0';
                a += digit * (int)Math.Pow(2, degree);
                degree--;
            }

            output += (char)a;
        }

        return output;
    }
}