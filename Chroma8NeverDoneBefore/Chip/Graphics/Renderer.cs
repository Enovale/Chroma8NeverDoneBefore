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
                _context.Memory.SetRange((ushort) (DefaultFont.FontOffset + i * DefaultFont.FontSize),
                    DefaultFont.HexChars[i]);
            }

            return (ushort) (DefaultFont.HexChars.Length * DefaultFont.FontSize);
        }

        public void Clear()
        {
            FrameBuffer = new bool[ScreenWidth, ScreenHeight];
        }

        public void DrawSprite(byte rX, byte rY, byte n)
        {
            _context.Processor.Registers[0xF] = 0x00;
            for (int y = rY; y < rY + n; y++)
            {
                var line = _context.Memory.Read((ushort) (_context.Processor.MemRegister + (y - rY)));
                for (int x = rX; x < rX + 8; x++)
                {
                    var posX = x % ScreenWidth;
                    var posY = y % ScreenHeight;
                    var old = FrameBuffer[posX, posY];
                    var value = ((line << (x - rX)) & 0x80) != 0;
                    FrameBuffer[posX, posY] ^= value;
                    if (old && !FrameBuffer[posX, posY])
                        _context.Processor.Registers[0xF] = 0x01;
                }
            }

        }
    }
}