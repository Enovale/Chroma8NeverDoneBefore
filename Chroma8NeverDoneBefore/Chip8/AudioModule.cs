namespace Chroma8NeverDoneBefore.Chip8
{
    public class AudioModule
    {
        public byte DelayTimer;
        public byte SoundTimer;
        public bool PlayTone => SoundTimer > 0;

        private Chip8 _context;

        public AudioModule(Chip8 context)
        {
            _context = context;
        }

        public void Update()
        {
            if (DelayTimer > 0)
                DelayTimer--;
            if (SoundTimer > 0)
                SoundTimer--;
        }
    }
}