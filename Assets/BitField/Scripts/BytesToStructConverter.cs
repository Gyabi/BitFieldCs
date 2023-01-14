using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace Bitfield
{
    /// <summary>
    /// byte[]から構造体への変換処理を記述したクラス
    /// </summary>
    public class BytesToStructConverter
    {


        public BytesToStructConverter()
        {
        }

        /// <summary>
        /// byte配列をBitFieldを反映させた構造体へ変換する。
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Execute<T>(byte[] data) where T : struct
        {
            T output = ConvertToStructRecursive<T>(data);

            return output;
        }

        /// <summary>
        /// byte配列を構造体へ変換する
        /// 再帰的に走査する
        /// </summary>
        /// <param name="data">T型の構造体に必要なbit長のbyte配列</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ConvertToStructRecursive<T>(byte[] data) where T : struct
        {
            // 出力用の変数を作成
            T output = new T();

            // 構造体に含まれる要素を検索してstructへ変換していく
            // foreach(FieldInfo fieldInfo in output.GetType().GetFields().OrderBy(field => field.MetadataToken))
            // {
            //     // データが構造体なら再帰的に実行
            //     if(BFUtils.IsStruct(fieldInfo.GetValue(output).GetType()))
            //     {
            //         Type type = fieldInfo.GetValue(output).GetType();
            //         ConvertToStructRecursive<type>(data);
            //     }
            //     else
            //     {
            //         // アトリビュートがついていることを判定
            //         object[] attrs = fieldInfo.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
            //         if(attrs.Length == 1)
            //         {
            //             this.ReflectStructData(fieldInfo.GetValue(data),  ((BitfieldLengthAttribute)attrs[0]).Length);
            //         }
            //     }
            // }

            return output;
        }
    }
}