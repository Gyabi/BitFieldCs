using System.Collections;
using System;
using UnityEngine;

namespace Bitfield
{
    public class BitFieldDebug : MonoBehaviour
    {
        private void Start()
        {
            SampleStruct sample = new SampleStruct();
            sample.sample1 = 256;
            sample.sample2 = 3;
            sample.sample3 = new SampleStruct2();
            sample.sample3.sample1 = 3;
            sample.sample3.sample2 = 3;
            sample.sample5 = 1;

            byte[] bytes = BFConverterHelper.ToBytes(sample);

            // byte[] bytes = PrimitiveConversion.ToBytes(sample);

            // Debug.Log(BitConverter.ToString(bytes));
            string text = "";
            string tmp = "";

            foreach(byte b in bytes)
            {
                string s = Convert.ToString(b, 2);
                text = s.PadLeft(8, '0');
                tmp += " " + text;
            }

            Debug.Log(tmp);

            Debug.Log(BitConverter.ToString(bytes));

            // int a = 256;
            // byte[] bytes2 = BitConverter.GetBytes(a);
            // BitArray bits = new BitArray(bytes2);

            // bits.Set(0, true);
            // bits.LeftShift(3);

            // string output = "";
            // foreach(bool bit in bits)
            // {
            //     output += bit ? "1" : "0";
            // }

            // Debug.Log(output);
        }
    }
}