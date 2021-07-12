namespace Chroma8NeverDoneBefore.Chip
{
    public interface IPlatform
    {
        public bool IsKeyDown(byte key);
        public bool IsKeyUp(byte key);
        public byte AnyKeyDown();
    }
}