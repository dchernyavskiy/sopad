namespace LB1.Services.Contracts;

public interface IDataProcessor
{
    string Encrypt(string key, string plaintext);
    string Decrypt(string key, string plaintext);
}