namespace LB1.Models.Responses;

public record EncryptResponse(byte[] EncryptedText, IDictionary<int, double> Entropies);