namespace Bitfield
{
    public static class BFConverterHelper
    {
        public static byte[] ToBytes<T>(T data) where T : struct
        {
            BFConverter converter = new BFConverter();

            return converter.StructToBytes(data);
        }

        public static void FromByte<T>(byte[] data) where T : struct
        {
        }
    }
}