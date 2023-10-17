namespace LB1.Services.Contracts;

public interface IKeyGenerator
{
    ulong[] GenerateKeySchedule(ulong key);
    void CheckForWeakKeys(ulong[] keySchedule);
}