namespace LB5.Models;

public record HashRequest(string Data, int BitSize);

public record HashResponse(byte Hash);