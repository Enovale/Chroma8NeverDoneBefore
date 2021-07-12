using System;

namespace Chroma8NeverDoneBefore.Chip
{
    public class AudioModule
    {
        public byte DelayTimer;
        public byte SoundTimer;
        public bool PlayTone => SoundTimer > 0;

        private float LeftoverTimer = 0;

        private Chip8 _context;

        public AudioModule(Chip8 context)
        {
            _context = context;
        }

        public void Update(float deltaTime)
        {
            deltaTime *= 1000;
            deltaTime += LeftoverTimer;
            LeftoverTimer = deltaTime % 60;

            var t = MathF.Floor(deltaTime / 60);
            for (var i = 0; i < t; i++)
            {
                if (DelayTimer > 0)
                    DelayTimer--;
                if (SoundTimer > 0)
                    SoundTimer--;
            }
        }
    }
}