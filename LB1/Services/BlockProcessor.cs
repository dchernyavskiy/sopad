using System.Text;
using LB1.Constants;
using LB1.Services.Contracts;

namespace LB1.Services;

public class BlockProcessor : IBlockProcessor
{
    private readonly IFeistelService _feistelService;

    public BlockProcessor(IFeistelService feistelService)
    {
        _feistelService = feistelService;
    }

    public string EncryptBlock(string plaintextBlock, ulong[] K)
    {
        if (plaintextBlock.Length != 64)
            throw new Exception("Input block length is not 64 bits!");

        string outBlock = string.Empty;
        for (int i = 0; i < Tables.IP.Length; i++)
        {
            outBlock += plaintextBlock[Tables.IP[i] - 1];
        }

        string mL = outBlock.Substring(0, 32);
        string mR = outBlock.Substring(32);

        for (int i = 0; i < 16; i++)
        {
            string curKey = Convert.ToString((long)K[i], 2).PadLeft(48, '0');
            string fResult = _feistelService.F(mR, curKey);
            long fVal = Convert.ToInt64(fResult, 2);
            long cmL = Convert.ToInt64(mL, 2);

            long m2 = cmL ^ fVal;
            string m2String = Convert.ToString(m2, 2).PadLeft(32, '0');

            mL = mR;
            mR = m2String;
        }

        string inBlock = mR + mL;
        string output = string.Empty;
        for (int i = 0; i < Tables.IPi.Length; i++)
        {
            output += inBlock[Tables.IPi[i] - 1];
        }

        return output;
    }

    public string DecryptBlock(string plaintextBlock, ulong[] K)
    {
        if (plaintextBlock.Length != 64)
            throw new Exception("Input block length is not 64 bits!");

        string outBlock = string.Empty;
        for (int i = 0; i < Tables.IP.Length; i++)
        {
            outBlock += plaintextBlock[Tables.IP[i] - 1];
        }

        string mL = outBlock.Substring(0, 32);
        string mR = outBlock.Substring(32);

        for (int i = 16; i > 0; i--)
        {
            string curKey = Convert.ToString((long)K[i], 2).PadLeft(48, '0');
            string fResult = _feistelService.F(mR, curKey);
            long fVal = Convert.ToInt64(fResult, 2);
            long cmL = Convert.ToInt64(mL, 2);

            long m2 = cmL ^ fVal;
            string m2String = Convert.ToString(m2, 2).PadLeft(32, '0');

            mL = mR;
            mR = m2String;
        }

        string inBlock = mR + mL;
        string output = string.Empty;
        for (int i = 0; i < Tables.IPi.Length; i++)
        {
            output += inBlock[Tables.IPi[i] - 1];
        }

        return output;
    }
}