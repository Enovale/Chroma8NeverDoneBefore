namespace Chroma8NeverDoneBefore.Chip8.CPU
{
    public class Processor
    {
        public ushort MemRegister;
        public byte[] Registers;

        public ushort ProgramCounter;
        public ushort StackPointer;
        public ushort[] Stack;

        private Chip8 _context;

        public Processor(Chip8 context)
        {
            _context = context;
            Registers = new byte[16];
            Stack = new ushort[16];
        }

        public void Clock()
        {
            
        }
    }
}