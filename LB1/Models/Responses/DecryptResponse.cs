namespace LB1.Models.Responses;

public record DecryptResponse(string DecryptedText, IDictionary<int, double> Entropies);