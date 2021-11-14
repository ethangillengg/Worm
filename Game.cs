using System;
using System.Threading;
using System.Collections.Generic;
public class Game : GameBoard
{
    private static int _defaultWormLength = 5;
    private List<Fruit> _fruits;
    private Worm _theWorm;
    private Thread _gameThread;
    public Game(int left, int top, int height):
    base(left, top, height)
    {
        _theWorm = new Worm(Left+Width/2, Top+Height/2, _defaultWormLength);
        
    }
    public void StartGame()
    {
        _gameThread = new Thread(RunGame);

        Console.SetCursorPosition(Left+1, Top-1);
        Console.Write("Worm length: {0}", _defaultWormLength);

        Console.SetCursorPosition(Left+Width/8, _theWorm.Top-3);
        Console.Write("Welcome to Worm, press any key to start!");
        Console.ReadKey(true);

        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("3...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("2...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("1...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("Go!   ");
        Thread.Sleep(1000);
        Console.SetCursorPosition(Left+Width/8, _theWorm.Top-3);
        Console.Write("".PadRight(50));
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("".PadRight(6));

        DrawBoard();
        GenerateFruits(3,8);

        _gameThread.Start();
        GetInput();
    }
    private void RunGame()
    {
        while(true)
        {
            _theWorm.Advance();
            if(AteFruit())
            {
                _theWorm.Length++;
                Console.SetCursorPosition(Left+1, Top-1);
                Console.Write("Worm length: {0}", _theWorm.Length);
            }else if(GameOver())
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(Left+Width/4, Top+Height/2-3);
                Console.Write("You LOSE!!");
                Console.ResetColor();

                Console.SetCursorPosition(Left+Width/4, Top+Height/2-2);
                Console.Write("Press any key to restart");
                return;
            }
            Thread.Sleep(CalcSpeed());
        }
    }
    private bool GameOver()
    {
        if(_theWorm.Left <= Left) return true;
        if(_theWorm.Top <= Top) return true;
        if(_theWorm.Left >= Left+Width) return true;
        if(_theWorm.Top >= Top+Height) return true;
        foreach(WormSegment seg in _theWorm.Segments)
        {
            if(_theWorm.Top == seg.Top && _theWorm.Left == seg.Left) return true;
        }
        return false;
    }
    private bool AteFruit()
    {
        foreach(Fruit fruit in _fruits)
        {
            if(_theWorm.Top == fruit.Top && _theWorm.Left == fruit.Left)
            {
                _fruits.Remove(fruit);
                if(_fruits.Count == 0)
                {
                    GenerateFruits(3,8);
                }
                return true;
            }
        }
        return false;
    }
    
    private void GetInput()
    {
        
        while(_gameThread.IsAlive)
        {
            switch(Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                _theWorm.Input = ConsoleKey.W;
                break;
                case ConsoleKey.A:
                _theWorm.Input = ConsoleKey.A;
                break;
                case ConsoleKey.S:
                _theWorm.Input = ConsoleKey.S;
                break;
                case ConsoleKey.D:
                _theWorm.Input = ConsoleKey.D;
                break;
            }
        }
        Console.SetCursorPosition(Left+Width/4, Top+Height/2-3);
        Console.Write("".PadRight(50));
        Console.SetCursorPosition(Left+Width/4, Top+Height/2-2);
        Console.Write("".PadRight(50));
        
        Restart();
    }
    public void Restart()
    {
        Console.SetCursorPosition(Left+1, Top-1);
        Console.Write("".PadRight(40));
        _theWorm.EraseWorm();
        foreach(Fruit fruit in _fruits) fruit.Erase();
        _fruits = new List<Fruit>();
        DrawBoard();
        _theWorm = new Worm(Left+Width/2, Top+Height/2, _defaultWormLength);
        StartGame();
    }
    private void GenerateFruits(int min, int max)
    {
        _fruits = new List<Fruit>();
        Random random = new Random();
        int numberOfFruits = random.Next(min,max);
        while(numberOfFruits > 0)
        {
            _fruits.Add(new Fruit(Left, Top, Height));
            numberOfFruits--;
        }
    }
    private int CalcSpeed()
    {
        int fruitsComsumed = _theWorm.Length - _defaultWormLength;
        int delay = 250 - fruitsComsumed*10;
        if(delay<30) return 30;
        return delay;
    }
}