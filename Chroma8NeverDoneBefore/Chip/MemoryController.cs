using System;

namespace Chroma8NeverDoneBefore.Chip
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

        public void Clear() => Tape = new byte[Tape.Length];

        public byte Read(ushort position) => Tape[position];
        public byte[] ReadRange(Range range) => Tape[range];
        public ushort ReadUShort(ushort position) => BitConverter.ToUInt16(Tape, position);
        public uint ReadUInt(ushort position) => BitConverter.ToUInt32(Tape, position);
        public ulong ReadULong(ushort position) => BitConverter.ToUInt64(Tape, position);

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