using System.Collections;
using System;
using UnityEngine;

public class BitFieldDebug : MonoBehaviour
{
    private void Start()
    {
        SampleStruct sample = new SampleStruct();
        sample.sample1 = 1;
        sample.sample2 = 2;

        byte[] bytes = PrimitiveConversion.ToBytes(sample);

        Debug.Log(BitConverter.ToString(bytes));
        string text = "";
        string tmp = "";

        foreach(byte b in bytes)
        {
            string s = Convert.ToString(b, 2);
            text = s.PadLeft(8, '0');
            tmp += " " + text;
        }

        Debug.Log(tmp);

        int a = 256;
        byte[] bytes2 = BitConverter.GetBytes(a);
        BitArray bits = new BitArray(bytes2);

        bits.Set(0, true);
        bits.LeftShift(3);

        string output = "";
        foreach(bool bit in bits)
        {
            output += bit ? "1" : "0";
        }

        Debug.Log(output);
    }
}
