using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Services.Contracts;

public interface IDesService
{
    // string Decrypt(string encryptedMessage, string key);
    // string Encrypt(string message, string key);
    string Decrypt(ReadOnlySpan<byte> encryptedMessage, string key);
    byte[] Encrypt(string message, string key);
    IDictionary<int, double> Entropies { get; } 
}