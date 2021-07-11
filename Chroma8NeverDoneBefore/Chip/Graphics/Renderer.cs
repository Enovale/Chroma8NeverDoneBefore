using System.Collections;
using System.Linq;
using Chroma8NeverDoneBefore.Helpers;

namespace Chroma8NeverDoneBefore.Chip.Graphics
{
    public class Renderer
    {
        public uint ScreenWidth;
        public uint ScreenHeight;

        public bool[,] FrameBuffer;
        
        private readonly Chip8 _context;
        
        public Renderer(Chip8 context)
        {
            _context = context;
        }

        public ushort Initialize(uint screenWidth = 64, uint screenHeight = 32)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            FrameBuffer = new bool[ScreenWidth, ScreenHeight];
            
            for (var i = 0; i < DefaultFont.HexChars.Length; i++)
            {
                _context.Memory.SetRange((ushort) (DefaultFont.FontOffset + i * DefaultFont.HexChars[i].Length),
                    DefaultFont.HexChars[i]);
            }

            return (ushort) (DefaultFont.HexChars.Length * DefaultFont.HexChars.First().Length);
        }

        public void Clear()
        {
            FrameBuffer = new bool[ScreenWidth, ScreenHeight];
        }

        public void DrawSprite(byte rX, byte rY, byte n)
        {
            var offsetX = _context.Processor.Registers[rX];
            var offsetY = _context.Processor.Registers[rY];
            for (var y = 0; y < n; y++)
            {
                var line = _context.Memory.Read((ushort) (_context.Processor.MemRegister + y));
                for (var x = 0; x < 8; x++)
                {
                    var posX = (offsetX + x) % ScreenWidth;
                    var posY = (offsetY + y) % ScreenHeight;
                    var value = ((line << x) & 0x80) != 0;
                    if (FrameBuffer[posX, posY] != value)
                        _context.Processor.Registers[0xF] = 0x01;
                    FrameBuffer[posX, posY] ^= value;
                }
            }
        }
    }
}