using Chroma.Input;
using Chroma8NeverDoneBefore.Chip;

namespace Chroma8NeverDoneBefore
{
    public class ChromaPlatform : IPlatform
    {
        public readonly KeyCode[] Keys = new[]
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F
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