namespace LB1.Services.Contracts;

public interface IBlockProcessor
{
    string EncryptBlock(string plaintextBlock, ulong[] K);
    string DecryptBlock(string plaintextBlock, ulong[] K);
}