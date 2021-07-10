using Chroma8NeverDoneBefore.Chip8.CPU;
using Chroma8NeverDoneBefore.Chip8.Graphics;

namespace Chroma8NeverDoneBefore.Chip8
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

        public void Update()
        {
            Audio.Update();
            Processor.Clock();
        }
    }
}