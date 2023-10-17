namespace LB1.Models.Requests;

public record DecryptRequest(string CipheredText, string Key);