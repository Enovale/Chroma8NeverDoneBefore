using System;

namespace Chroma8NeverDoneBefore.Chip8
{
    public class MemoryController
    {
        private byte[] Tape;
        private Chip8 _context;

        public MemoryController(Chip8 context)
        {
            _context = context;
            Tape = new byte[1024 * 4];
        }

        public byte Read(ushort position) => Tape[position];
        public byte[] ReadRange(Range range) => Tape[range];

        public ushort Set(ushort position, byte data)
        {
            Tape[position] = data;
            return (ushort) (position + 1);
        }

        public ushort SetRange(ushort position, byte[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                Tape[position + i] = data[i];
            }

            return (ushort) (position + data.Length);
        }
    }
}