namespace LB1.Services.Contracts;

public interface IConversionService
{
    long Hash(string input);
    string BinToHex(string binary);
    string HexToBinary(string hexStr);
    string UtfToBin(string utfStr);
    string BinToUTF(string input);
}