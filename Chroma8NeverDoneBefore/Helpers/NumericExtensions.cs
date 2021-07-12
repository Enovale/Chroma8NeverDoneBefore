using System.Collections.Generic;

namespace Chroma8NeverDoneBefore.Helpers
{
    public static class NumericExtensions
    {
        public static ushort SwapBytes(this ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }
        
        public static List<byte> GetDigits(this int source)
        {
            var digits = new List<byte>();
            var str = source.ToString();
            for (var i = 0; i < str.Length; i++)
            {
                digits.Add(byte.Parse(str.Substring(i, 1)));
            }

            return digits;
        }
    }
}