using System.Numerics;
using LB1.Constants;
using LB1.Services.Contracts;

namespace LB1.Services;

public class KeyGenerator : IKeyGenerator
{
    private int[] ParseBinaryString(string binaryString)
    {
        int[] result = new int[binaryString.Length];
        for (int i = 0; i < binaryString.Length; i++)
        {
            result[i] = binaryString[i] - '0';
        }

        return result;
    }


    public ulong[] GenerateKeySchedule(ulong key)
    {
        string binaryKey = Convert.ToString((long)key, 2).PadLeft(64, '0');
        string keyPC1 = string.Join("", Tables.PC1);
        string binKeyPC1 = string.Empty;

        for (int i = 0; i < keyPC1.Length; i++)
        {
            binKeyPC1 += binaryKey[keyPC1[i] - 1];
        }

        string leftHalf = binKeyPC1.Substring(0, 28);
        string rightHalf = binKeyPC1.Substring(28, 28);

        int iL = Convert.ToInt32(leftHalf, 2);
        int iR = Convert.ToInt32(rightHalf, 2);

        List<ulong> keySchedule = new List<ulong>();

        for (int i = 0; i < Tables.KeyShifts.Length; i++)
        {
            iL = (int)RotateLeft((ulong)iL, Tables.KeyShifts[i], 28);
            iR = (int)RotateLeft((ulong)iR, Tables.KeyShifts[i], 28);

            ulong merged = ((ulong)iL << 28) + (ulong)iR;
            string mergedStr = Convert.ToString((long)merged, 2).PadLeft(56, '0');

            string binKeyPC2 = string.Empty;

            foreach (int bitIndex in Tables.Vector)
            {
                binKeyPC2 += mergedStr[bitIndex - 1];
            }

            ulong subKey = Convert.ToUInt64(binKeyPC2, 2);
            keySchedule.Add(subKey);
        }

        return keySchedule.ToArray();
    }

    private static ulong RotateLeft(ulong value, int shift, int size)
    {
        return (value << shift | value >> (size - shift)) & ((1UL << size) - 1);
    }

    public void CheckForWeakKeys(ulong[] keySchedule)
    {
        for (int i = 0; i < keySchedule.Length; i++)
        {
            string strK = Convert.ToString((long)keySchedule[i], 16);

            foreach (string wK in Tables.WeakKeys)
            {
                if (strK.Contains(wK))
                {
                    int updatedK = (int)RotateLeft(keySchedule[i], 1, wK.Length * 4);
                    keySchedule[i] = (ulong)updatedK;
                }
            }
        }
    }
}