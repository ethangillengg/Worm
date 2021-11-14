using System;
using System.Threading;
using System.Collections.Generic;
public class Game : GameBoard
{
    private static int _defaultWormLength = 5;
    private List<Fruit> _fruits;
    private Worm _theWorm;
    private bool _gameRunning = false;
    private Thread _gameThread;
    public Game(int left, int top, int height):
    base(left, top, height)
    {
        _theWorm = new Worm(Left+Width/2, Top+Height/2, _defaultWormLength);

        Console.SetCursorPosition(Left+1, Top-1);
        Console.Write("Worm length: {0}", _defaultWormLength);
        
        GenerateFruits(3,8);

        _gameThread = new Thread(RunGame);
    }
    public void StartGame()
    {
        _gameThread.Start();
        _gameRunning = true;
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
                return;
            }
            Thread.Sleep(250);
        }
    }
    private bool GameOver()
    {
        
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
        // Restart();
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
}