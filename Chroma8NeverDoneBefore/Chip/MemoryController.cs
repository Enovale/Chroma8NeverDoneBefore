using System;
using System.Buffers.Binary;

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
        public byte ReadRelative(ushort position) => Tape[_context.ProgramOffset + position];
        public byte[] ReadRange(Range range) => Tape[range];
        public ushort ReadUShort(ushort position) => BinaryPrimitives.ReadUInt16BigEndian(Tape[position..]);
        public uint ReadUInt(ushort position) => BinaryPrimitives.ReadUInt32BigEndian(Tape[position..]);
        public ulong ReadULong(ushort position) => BinaryPrimitives.ReadUInt64BigEndian(Tape[position..]);

        public ushort Set(ushort position, byte data)
        {
            Tape[position] = data;
            return (ushort) (position + 1);
        }

        public ushort SetRange(ushort position, byte[] data)
        {
            Array.Copy(data, 0, Tape, position, data.Length);

            return (ushort) (position + data.Length);
        }
    }
}