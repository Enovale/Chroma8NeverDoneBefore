namespace Chroma8NeverDoneBefore.Chip8.Graphics
{
    public class Renderer
    {
        public uint ScreenWidth;
        public uint ScreenHeight;
        
        private Chip8 _context;
        
        public Renderer(Chip8 context)
        {
            _context = context;

            for (var i = 0; i < DefaultFont.HexChars.Length; i++)
            {
                _context.Memory.SetRange((ushort) (DefaultFont.FontOffset + i), DefaultFont.HexChars[i]);
            }
        }
    }
}