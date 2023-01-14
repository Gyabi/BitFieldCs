using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Bitfield
{
    public static class BFUtils
    {
        /// <summary>
        /// 構造体かどうか判定
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStruct(System.Type type)
        {
            return type.IsValueType &&  // 値型に限定（classを除外）
                !type.IsPrimitive &&    // intやfloatなどを除外
                !type.IsEnum;           // enumを除外（enumはValueTypeだけどPrimitiveではない）
        }

        /// <summary>
        /// object型をbyte[]へ変換する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object data)
        {
            var size = Marshal.SizeOf(data);
            // Both managed and unmanaged buffers required.
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            // Copy object byte-to-byte to unmanaged memory.
            Marshal.StructureToPtr(data, ptr, false);
            // Copy data from unmanaged memory to managed buffer.
            Marshal.Copy(ptr, bytes, 0, size);
            // Release unmanaged memory.
            Marshal.FreeHGlobal(ptr);

            return bytes;
        }

        /// <summary>
        /// byte[]を任意の型へ変換する
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ByteArrayToObject<T>(byte[] data)
        {
            var size = Marshal.SizeOf(data);
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);
            var your_object = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return your_object;
        }

        /// <summary>
        /// BitArrayをbyteへ変換する
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static byte BitArrayToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}