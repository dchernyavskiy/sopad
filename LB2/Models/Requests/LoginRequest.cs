namespace LB2.Models.Requests;

public record LoginRequest(string EncryptedLogin, string EncryptedPassword);