using System;

namespace Boids
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BoidGame())
                game.Run();
        }
    }
}
