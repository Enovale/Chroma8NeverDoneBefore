namespace Chroma8NeverDoneBefore.Helpers
{
    public static class NumericExtensions
    {
        public static byte SwapBytes(this byte x)
        {
            return (byte)((byte)((x & 0xff) << 4) | ((x >> 4) & 0xff));
        }
        public static ushort SwapBytes(this ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }
    }
}