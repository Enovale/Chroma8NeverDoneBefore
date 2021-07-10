namespace Chroma8NeverDoneBefore.Chip.CPU
{
    public class Processor
    {
        public ushort MemRegister;
        public byte[] Registers;

        public ushort ProgramCounter;
        public int StackPointer;
        public ushort[] Stack;

        private Chip8 _context;

        public Processor(Chip8 context)
        {
            _context = context;
        }

        public void Initialize(ushort PcStart = 0x000)
        {
            Registers = new byte[16];
            Stack = new ushort[16];
            ProgramCounter = PcStart;
            StackPointer = 0;
        }

        public void Clock()
        {
            
        }
    }
}