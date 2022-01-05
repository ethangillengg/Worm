using System;

class Program
{
    static void Main(string[] args)
    {
        //Clear the console to make sure there is nothing on the screen
        Console.Clear();
        Console.CursorVisible = false;
        Console.ReadKey();
        
        //Create a new game and start it
        var wormGame = new WormGame(10, 3, 60, 30);
        wormGame.StartGame();
    }
}

