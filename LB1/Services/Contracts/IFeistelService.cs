namespace LB1.Services.Contracts;

public interface IFeistelService
{
    string F(string mi, string key);
    double CountEntropy(string mergedP);
}