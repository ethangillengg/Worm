using System;

namespace WormGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var game = new Game(10,3,20);
            game.StartGame();
        }
    }
}
