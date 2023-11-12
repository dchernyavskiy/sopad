namespace LB3.Models;

public record HashRequest(string Data, int BitSize);

public record HashResponse(byte Hash);