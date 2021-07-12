namespace Chroma8NeverDoneBefore.Chip
{
    public class InputHandler
    {
        private IPlatform _platform;
        private Chip8 _context;
        
        public InputHandler(Chip8 context)
        {
            _context = context;
        }

        public void Initialize(IPlatform plat)
        {
            _platform = plat;
        }

        public byte WaitForAnyKey()
        {
            while (true)
            {
                var key = _platform.AnyKeyDown();
                if (key != 0xFF)
                    return key;
            }
        }

        public bool IsKeyDown(byte key) => _platform.IsKeyDown(key);
        public bool IsKeyUp(byte key) => _platform.IsKeyUp(key);
    }
}