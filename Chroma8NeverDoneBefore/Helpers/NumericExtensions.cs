namespace Chroma8NeverDoneBefore.Helpers
{
    public static class NumericExtensions
    {
        public static ushort SwapBytes(this ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }
    }
}