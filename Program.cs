using System;

namespace WormGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            var game = new Game(10,3,25);
            game.StartGame();
        }
    }
}
