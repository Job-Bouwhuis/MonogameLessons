using System;

namespace CSharpAdvanced
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Assignment2.Game1();
            game.Run();
        }
    }
}
