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

        public Chip8()
        {
            Processor = new Processor(this);
            Memory = new MemoryController(this);
            Audio = new AudioModule(this);
            Input = new InputHandler(this);
            Renderer = new Renderer(this);
        }

        public void Load(byte[] data)
        {
            Memory.Clear();

            Renderer.Initialize();
            Memory.SetRange(0x200, data);
            Processor.Initialize(0x200);
        }

        public void LoadFile(string path) => Load(File.ReadAllBytes(path));

        public void Update(float deltaTime)
        {
            Audio.Update(deltaTime);
            Processor.Clock();
        }
    }
}