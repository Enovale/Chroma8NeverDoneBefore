using System;
using System.Linq;
using System.Numerics;
using Chroma;
using Chroma.Audio;
using Chroma.Audio.Sources;
using Chroma.Graphics;
using Chroma8NeverDoneBefore.Chip;

namespace Chroma8NeverDoneBefore
{
    public class Game : Chroma.Game
    {
        private readonly RenderTarget _target;
        private readonly Chip8 _system;
        private readonly Waveform _tone;

        public Game(GameStartupOptions options = null) : base(options)
        {
            _system = new Chip8(new ChromaPlatform());
            _system.LoadFile(@"D:\Documents\GitHub\Chroma8NeverDoneBefore\Chroma8NeverDoneBefore\ROMs\pong.ch8");
            _target = new RenderTarget((int) _system.Renderer.ScreenWidth, (int) _system.Renderer.ScreenHeight)
            {
                FilteringMode = TextureFilteringMode.NearestNeighbor
            };
            _tone = new Waveform(AudioFormat.Default, (data, format) => data.Fill(0x20), ChannelMode.Mono);
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.Gray);
            for (var y = 0; y < _system.Renderer.FrameBuffer.GetLength(1); y++)
            {
                for (var x = 0; x < _system.Renderer.FrameBuffer.GetLength(0); x++)
                {
                    _target[x, y] = _system.Renderer.FrameBuffer[x, y] ? Color.White : Color.Black;
                }
            }

            _target.Flush();
            context.DrawTexture(_target, Vector2.Zero, new Vector2(6, 6), Vector2.Zero, 0);
        }

        protected override void Update(float delta)
        {
            _system.Update(delta);
            if (_system.Audio.PlayTone && !_tone.IsPlaying)
                _tone.Play();
            else if (!_system.Audio.PlayTone && _tone.IsPlaying)
                _tone.Pause();
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