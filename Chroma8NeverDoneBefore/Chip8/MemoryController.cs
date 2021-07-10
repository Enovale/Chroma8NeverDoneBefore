namespace Chroma8NeverDoneBefore.Chip8
{
    public class MemoryController
    {
        private byte[] Tape;

        public MemoryController()
        {
            Tape = new byte[1024 * 4];
        }
    }
}