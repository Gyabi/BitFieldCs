using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Linq;

public class SampleCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        A a = new A();
        a.sample1=1;
        a.sample2=1;
        a.sample3=1;
        a.sample4 = new B();
        a.sample4.sample1=1;
        a.sample4.sample2=1;
        a.sample5=1;

        AConverter converter = new AConverter();
System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
sw.Start();

    // 構造体をbyte[]へ変換する
    
    byte[] bytes = converter.StructToBytes(a);
sw.Stop();
Debug.Log(sw.ElapsedMilliseconds + "ms");

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
    }
}

public class AConverter
{
    // 出力するデータを作成
    byte[] output = new byte[2];
    int arrayIndex = 0;
    int bitIndex = 0;

    public byte[] StructToBytes(A structData)
    {

        // 各bit長を反映させていく
        // output[0] = this.RefrectBits(output[0], BitConverter.GetBytes(structData.sample1), 1, 0);
        // output[0] = this.RefrectBits(output[0], BitConverter.GetBytes(structData.sample2), 1, 1);
        // output[0] = this.RefrectBits(output[0], BitConverter.GetBytes(structData.sample3), 2, 2);
        // output[0] = this.RefrectBits(output[0], BitConverter.GetBytes(structData.sample4.sample1), 2, 4);
        // output[0] = this.RefrectBits(output[0], BitConverter.GetBytes(structData.sample4.sample2), 2, 6);
        
        // output[1] = this.RefrectBits(output[1], BitConverter.GetBytes(structData.sample5), 8, 0);

        return output;
    }

    public void RefrectBits(byte[] member, int bitLength)
    {
        // マスク作成
        BitArray mask = new BitArray(member.Length, false);
        Enumerable.Range(0, bitLength-1).ToList().ForEach(i => mask.Set(i, true));
        // マスク反映
        BitArray memberBitArray = new BitArray(member);
        memberBitArray.And(mask);

        // ビットシフト
        memberBitArray.LeftShift(this.bitIndex);

        // byte[] bytes = (byte[])member.Clone();
        // memberBitArray.CopyTo(bytes, 0);

        // this.output |= bytes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">反映先のbyteデータ</param>
    /// <param name="member">対象の構造体メンバデータ</param>
    /// <param name="bitLength">使用するbit長</param>
    /// <param name="targetStartIndex">targetの何bit目から反映させるか</param>
    /// <returns></returns>
    // public byte RefrectBits(byte target, byte[] member, int bitLength, int targetStartIndex)
    // {
    //     // 反映先のbyteデータのindexが足りない場合
    //     if(bitLength + targetStartIndex - 1 > 7)
    //     {
    //         throw new System.Exception("bit長がオーバーフローしています");
    //     }

    //     // マスク作成
    //     BitArray mask = new BitArray(member.Length, false);
    //     Enumerable.Range(0, bitLength-1).ToList().ForEach(i => mask.Set(i, true));
    //     // マスク反映
    //     BitArray memberBitArray = new BitArray(member);
    //     memberBitArray.And(mask);
    //     // ビットシフト
    //     memberBitArray.LeftShift(targetStartIndex);
    //     // targetに反映
    //     target |= Bitfield.BFUtils.BitArrayToByte();

    //     return target;
    // }
    // public A BytesToStruct(byte[] data)
    // {

    // }
} 
public struct A
{
    // 1bit
    public int sample1;
    // 1bit
    public uint sample2;
    // 2bit
    public uint sample3;
    public B sample4;
    // 8bit
    public uint sample5;
}

public struct B
{
    // 2bit
    public int sample1;
    // 2bit
    public uint sample2;
}
