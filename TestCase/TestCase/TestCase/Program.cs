using System;

namespace TestCase
{
#if WINDOWS || XBOX
    static class Program
    {
        static bool m_TestCase = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (m_TestCase)
            {
                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }
            else
            {
                using (Editor game = new Editor())
                {
                    game.Run();
                }
            }
        }
    }
#endif
}

