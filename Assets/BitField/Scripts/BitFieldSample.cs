using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.InteropServices;


[global::System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
sealed class BitfieldLengthAttribute : Attribute
{
    uint length;

    public BitfieldLengthAttribute(uint length)
    {
        this.length = length;
    }

    public uint Length{ get { return length;}}
}

static class PrimitiveConversion
{
    public static byte[] ToBytes<T>(T t) where T : struct
    {
        List<byte> output = new List<byte>();
        byte currentByte = new byte();
        int offset = 0;
        int offsetStruct = 0;


        foreach(System.Reflection.FieldInfo f in t.GetType().GetFields().OrderBy(field => field.MetadataToken))
        {
            object[] attrs = f.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
            if(attrs.Length == 1)
            {
                uint fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;

                if(fieldLength > Marshal.SizeOf(f.GetValue(t))*8)
                {
                    throw new System.Exception("ビットフィールドの設定値が既定が他のサイズを超えています");
                }

                Type type = f.GetValue(t).GetType();

                if(offsetStruct + fieldLength > 32)
                {

                }
                else
                {
                    
                }
            }
        }

        return output.ToArray();
    }

    public static long ToLong<T>(T t)where T : struct
    {
        long r = 0;
        int offset = 0;
        foreach(System.Reflection.FieldInfo f in t.GetType().GetFields().OrderBy(field => field.MetadataToken))
        {
            object[] attrs = f.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
            if(attrs.Length == 1)
            {
                uint fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;

                long mask = 0;
                for(int i=0; i<fieldLength; i++)
                {
                    mask |= 1 << i;
                }

                r |= ((UInt32)f.GetValue(t) & mask) << offset;

                offset += (int)fieldLength;
            }
        }

        return r;
    }
}

public struct SampleStruct
{
    [BitfieldLength(2)]
    public int sample1;
    [BitfieldLength(1)]
    public uint sample2;
}