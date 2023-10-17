using LB1.Constants;
using LB1.Services.Contracts;

namespace LB1.Services;

public class FeistelService : IFeistelService
{
    public string F(string mi, string key)
    {
        string eMi = string.Concat(Tables.E.Select(index => mi[index - 1]));

        long m = Convert.ToInt64(eMi, 2);
        long k = Convert.ToInt64(key, 2);

        long result = m ^ k;

        string bin = Convert.ToString(result, 2).PadLeft(48, '0');

        string[] sin = new string[8];
        for (int i = 0; i < 8; i++)
        {
            sin[i] = bin.Substring(6 * i, 6 * (i + 1));
        }

        string[] sout = new string[8];
        for (int i = 0; i < 8; i++)
        {
            int[][] curS = Tables.S[i];
            string cur = sin[i];

            var row = Convert.ToInt64(cur[0].ToString() + cur[5].ToString(), 2);
            var col = Convert.ToInt64(cur.Substring(1, 4), 2);
            sout[i] = Convert.ToString(curS[row][col], 2).PadLeft(4, '0');
            while (sout[i].Length < 4)
            {
                sout[i] = "0" + sout[i];
            }
        }

        string merged = string.Concat(sout);

        string mergedP = string.Concat(Tables.P.Select(index => merged[index - 1]));

        return mergedP;
    }

    public double CountEntropy(string mergedP)
    {
        int onesCount = mergedP.Count(bit => bit == '1');
        double probability = (double)onesCount / mergedP.Length;
        double entropy = -probability * Math.Log(probability, 2) - (1 - probability) * Math.Log(1 - probability, 2);

        return entropy;
    }
}