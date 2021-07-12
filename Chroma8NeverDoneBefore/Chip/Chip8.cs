using System.IO;
using Chroma8NeverDoneBefore.Chip.Graphics;

namespace Chroma8NeverDoneBefore.Chip
{
    public class Chip8
    {
        public Processor Processor;
        public MemoryController Memory;
        public AudioModule Audio;
        public InputHandler Input;
        public Renderer Renderer;
        public ushort ProgramOffset;

        private IPlatform _platform;

        public Chip8(IPlatform plat)
        {
            _platform = plat;
            Processor = new Processor(this);
            Memory = new MemoryController(this);
            Audio = new AudioModule(this);
            Input = new InputHandler(this);
            Renderer = new Renderer(this);
        }

        public void Load(byte[] data)
        {
            ProgramOffset = 0x200;
            Memory.Clear();

            Renderer.Initialize();
            Input.Initialize(_platform);
            Memory.SetRange(ProgramOffset, data);
            Processor.Initialize(ProgramOffset);
        }

        public void LoadFile(string path) => Load(File.ReadAllBytes(path));

        public void Update(float deltaTime)
        {
            Audio.Update(deltaTime);
            Processor.Clock();
        }
    }
}