using Chroma.Input;
using Chroma8NeverDoneBefore.Chip;

namespace Chroma8NeverDoneBefore
{
    public class ChromaPlatform : IPlatform
    {
        public readonly KeyCode[] Keys = new[]
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Q,
            KeyCode.W,
            KeyCode.E,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.Z,
            KeyCode.X,
            KeyCode.C,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Space
        };

        public bool IsKeyDown(byte key)
        {
            return Keyboard.IsKeyDown(Keys[key]);
        }

        public bool IsKeyUp(byte key)
        {
            return Keyboard.IsKeyUp(Keys[key]);
        }

        public byte AnyKeyDown()
        {
            for (var i = 0; i < Keys.Length; i++)
            {
                if (Keyboard.IsKeyDown(Keys[i]))
                    return (byte) i;
            }

            return 0xFF;
        }
    }
}