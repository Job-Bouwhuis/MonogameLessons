using System;

namespace CSharpAdvanced
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Assignment1.Game1();
            game.Run();
        }
    }
}
