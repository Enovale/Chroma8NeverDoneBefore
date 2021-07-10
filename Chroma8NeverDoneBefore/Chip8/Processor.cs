namespace Chroma8NeverDoneBefore.Chip8
{
    public class Processor
    {
        public ushort MemRegister;
        public byte[] Registers;

        public ushort ProgramCounter;
        public ushort StackPointer;
        public ushort[] Stack;

        public Processor()
        {
            Registers = new byte[16];
            Stack = new ushort[16];
        }
    }
}