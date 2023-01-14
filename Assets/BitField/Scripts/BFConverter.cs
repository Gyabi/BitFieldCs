using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Bitfield
{
    public class BFConverter
    {
        // 返還後のbyte配列を保持するためのlist
        List<byte> outputs;
        // ビットの情報を保持する為の変数
        BitArray currentBits;
        // 今、操作しているビットのインデックス
        int currentIndex;

        public BFConverter()
        {
            this.outputs = new List<byte>();
            this.currentBits = new BitArray(8);
            this.currentIndex = 7;
        }

        /// <summary>
        /// 構造体をBitFieldを反映させたbyte配列へ変換する。
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public byte[] StructToBytes<T>(T data)
        {
            this.ConvertToBytesRecursive(data);

            if(this.currentIndex != 7)
            {
                this.outputs.Add(BFUtils.BitArrayToByte(this.currentBits));
            }
            return this.outputs.ToArray();
        }

        /// <summary>
        /// 構造体のデータをメンバへ反映させる
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private void ConvertToBytesRecursive<T>(T data)
        {
            // 構造体に含まれる要素を検索してbitFieldsへ格納していく
            foreach(FieldInfo fieldInfo in data.GetType().GetFields().OrderBy(field => field.MetadataToken))
            {
                // データが構造体なら再帰的に実行
                if(BFUtils.IsStruct(fieldInfo.GetValue(data).GetType()))
                {
                    ConvertToBytesRecursive(fieldInfo.GetValue(data));
                }
                else
                {
                    // アトリビュートがついていることを判定
                    object[] attrs = fieldInfo.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                    if(attrs.Length == 1)
                    {
                        this.ReflectStructData(fieldInfo.GetValue(data),  ((BitfieldLengthAttribute)attrs[0]).Length);
                    }
                }
            }
        }

        /// <summary>
        /// ビット配列へ反映
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        private void ReflectStructData(object data, uint length)
        {
            // bit長が既存のデータ型よりも長い場合は例外
            if(length > Marshal.SizeOf(data)*8)
            {
                throw new System.Exception("ビットフィールドの設定値が既定が他のサイズを超えています");
            }

            // object→byte[]→BitArrayに変換
            BitArray bits = new BitArray(BFUtils.ObjectToByteArray(data));

            // 処理するビットを順番に走査
            while(length != 0)
            {
                // メンバとして保存している現在のBitArrayへ1bit分反映
                this.currentBits.Set(this.currentIndex, bits.Get((int)length-1));
                // currentIndexの更新
                this.currentIndex--;
                // currentIndexが-1になっている（1byte分埋まった状態）ならデータを保存してリセット
                if(this.currentIndex == -1)
                {
                    this.outputs.Add(BFUtils.BitArrayToByte(this.currentBits));
                    this.currentBits.SetAll(false);
                    this.currentIndex = 7;
                }

                length--;
            }
        }
    }
}