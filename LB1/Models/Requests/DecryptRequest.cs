namespace LB1.Models.Requests;

public record DecryptRequest(byte[] CipheredText, string Key);