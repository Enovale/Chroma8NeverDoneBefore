using System;
using Chroma;

namespace Chroma8NeverDoneBefore
{
    class Program
    {
        private static void Main(string[] args) => new Game(new GameStartupOptions(false, false, 0)).Run();
    }
}