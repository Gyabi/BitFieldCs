namespace Bitfield
{
    /// <summary>
    /// BitFieldに関する変換ヘルパー
    /// </summary>
    public static class BFConverterHelper
    {
        /// <summary>
        /// 構造体からbyte[]への変換処理
        /// </summary>
        public static byte[] ToBytes<T>(T data) where T : struct
        {
            StructToBytesConverter converter = new StructToBytesConverter();

            return converter.Execute(data);
        }

        /// <summary>
        /// byte[]から構造体への変換処理
        /// </summary>
        public static T FromByte<T>(byte[] data) where T : struct
        {
            BytesToStructConverter converter = new BytesToStructConverter();

            return converter.Execute<T>(data);
        }
    }
}