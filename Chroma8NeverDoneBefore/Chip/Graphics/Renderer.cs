using System.Linq;

namespace Chroma8NeverDoneBefore.Chip.Graphics
{
    public class Renderer
    {
        public uint ScreenWidth;
        public uint ScreenHeight;
        
        private Chip8 _context;
        
        public Renderer(Chip8 context)
        {
            _context = context;
        }

        public ushort Initialize()
        {
            for (var i = 0; i < DefaultFont.HexChars.Length; i++)
            {
                _context.Memory.SetRange((ushort) (DefaultFont.FontOffset + (i * DefaultFont.HexChars[i].Length)), DefaultFont.HexChars[i]);
            }

            return (ushort) (DefaultFont.HexChars.Length * DefaultFont.HexChars.First().Length);
        }
    }
}