using Chroma;
using Chroma.Graphics;
using Chroma8NeverDoneBefore.Chip;

namespace Chroma8NeverDoneBefore
{
    public class Game : Chroma.Game
    {
        private Chip8 _system;
        
        public Game(GameStartupOptions options = null) : base(options)
        {
            _system = new Chip8();
        }

        protected override void Draw(RenderContext context)
        {
            base.Draw(context);
        }

        protected override void Update(float delta)
        {
            _system.Update(delta);
        }

        protected override void FixedUpdate(float delta)
        {
            base.FixedUpdate(delta);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}