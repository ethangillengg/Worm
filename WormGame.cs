using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
public class WormGame : GameBoard
{
/*The worm game
 *RESPONSIBILITES:
 * - Stores all entities in the game (fruits, the worm)
 * - Can start and restart a game
*/
    private static readonly int _defaultWormLength = 5; //Default length of the worm
    private static readonly Tuple<int, int> _numberOfFruit = new Tuple<int, int>(3,10); //The minimum and maximum number of fruit to be generated on the board
    private List<Fruit> _fruits = new List<Fruit>(); //A list of all the fruits in the game
    private Worm _theWorm; //The worm used in the game
    private Thread _gameThread; //A thread to run the game (the main thread is used for user inputs)
    public IList<Entity> Entities //A readonly list of all the entities currently in the game
    {
        get
        {
            var result = new List<Entity>();
            result = result.Concat(_fruits).Concat(_theWorm.Segments).ToList();
            return result.AsReadOnly();
        }
    }
    public WormGame(int left = 10, int top = 3, int width = 30, int height = 20):
    base(left, top, width, height)
    {
        //Make a worm that is placed in the middle of the board with a default length
        _theWorm = new Worm(Left+Width/2, Top+Height/2, _defaultWormLength); 
    }
    public void StartGame()
    {
        _gameThread = new Thread(RunGame); //Set the gamethread to a new thread that runs the game

        //Print stuff, then wait for user input
        PrintBoard();
        PrintWormLength();
        PrintWelcomeMessage();

        //Once a user presses a key, begin the countdown
        Console.ReadKey(true);
        StartCountdown(); 

        //Generate the fruits on the screen, and reprint the board
        GenerateFruits();
        PrintBoard();

        //Start the game
        _gameThread.Start(); //The thread running the game starts here
        GetInput(); //The main thread is sent to go get user inputs
    }
    private void PrintWelcomeMessage() //Prints a message welcoming the user
    {
        Console.SetCursorPosition(Left+Width/8, _theWorm.Top-3);
        Console.Write("Welcome to Worm, press any key to start!");
    }
    private void PrintWormLength() //Prints the current worm's length
    {
        Console.SetCursorPosition(Left+1, Top-1);
        Console.Write("Worm length: {0}".PadRight(20), _theWorm.Length);
    }
    private void StartCountdown() //Prints out a countdown above the worm signalling that the game is starting
    {
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
    }
    private void RunGame() //Advances the worm, checks for fruit eaten/game over, recalculates the delay, and then waits until the next turn
    {
        int delay = DelayBasedOnWormLength(); //The delay per turn in milliseconds
        Stopwatch frameTimer = new Stopwatch(); //Calculate how long each frame took to run
        while(true) //Run until the game is over (then it uses return to get out of the while loop)
        {
            frameTimer.Restart();
            Console.CursorVisible = false; //Make the cursor invisible (sometimes it goes visible when the window size of the terminal is changed)
            _theWorm.Advance(); //Advance the worm one tile

            if(CheckIfAteFruit())
            {
                //If fruit was eaten, increment the worm's length and 
                //reprint that length to the top of the screen
                _theWorm.Length+=3;
                PrintWormLength();
            }else if(CheckIfGameOver())
            {
                //If the game is over, print a lose screen and end the thread by returning the function
                PrintLoseScreen();
                return;
            }

            //Calculate the delay based on the worm length and how long the frame took to run
            //subtracting how long the frame took ensures that the delay is consistent
            //no matter how long the thread took to do the frame.
            delay = DelayBasedOnWormLength() - (int)frameTimer.ElapsedMilliseconds;
            if(delay < 0) delay = 0;

            //Wait until the next turn
            Thread.Sleep(delay);
        }
    }
    private void PrintLoseScreen() //Prints a lose screen
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(Left+Width/4, Top+Height/2-3);
        Console.Write("You LOSE!!");
        Console.ResetColor();

        Console.SetCursorPosition(Left+Width/4, Top+Height/2-2);
        Console.Write("Press any key to restart");
    }
    private void EraseLoseScreen() //Erases the lose screen
    {
        Console.SetCursorPosition(Left+Width/4, Top+Height/2-3);
        Console.Write("".PadRight(50));
        Console.SetCursorPosition(Left+Width/4, Top+Height/2-2);
        Console.Write("".PadRight(50));
    }
    private bool CheckIfGameOver() //Checks if the worm's head collided with a border of the game board or with one of it's own segments
    {
        //Check if the worm ran into a wall
        if(_theWorm.Left <= Left) return true;
        if(_theWorm.Top <= Top) return true;
        if(_theWorm.Left >= Left+Width) return true;
        if(_theWorm.Top >= Top+Height) return true;

        //Check if the worm ran into itself
        for(int i = 1; i<_theWorm.Length; i++)
        {
            var seg = _theWorm.Segments[i];
            if(_theWorm.Top == seg.Top && _theWorm.Left == seg.Left) return true;
        }

        //Return false if the worm didn't run into a wall or itself
        return false;
    }
    private bool CheckIfAteFruit()//Checks if the worm ate a fruit and removes it. If there are no fruit left, it generates more
    {
        foreach(Fruit fruit in _fruits)
        {
            if(_theWorm.Top == fruit.Top && _theWorm.Left == fruit.Left)
            {
                _fruits.Remove(fruit);
                CheckNoFruit();
                return true;
            }
        }
        return false;
    }
    private bool CheckNoFruit() //Checks if there are no fruit on the board, and creates more if there are none
    {
        if(_fruits.Count == 0)
        {
            GenerateFruits();
            return true;
        }
        return false;
    }
    private void GenerateFruits() //Generates a new set of fruits on the board
    {
        //Erases all the old fruits and makes a new list of fruits
        EraseFruits();
        _fruits = new List<Fruit>();


        //Generates a random number of fruits to place on the board
        Random random = new Random();
        int numberOfFruits = random.Next(_numberOfFruit.Item1, _numberOfFruit.Item2);

        //Creates the fruits and prints them on the board
        while(numberOfFruits > 0)
        {
            MakeARandomFruit();
            numberOfFruits--;
        }
    }
    private void MakeARandomFruit() //Creates a fruit at a random position on the board (not on top of another entity)
    {
        //Position where the new fruit will be placed
        int left = 0, top = 0; 
        //Random for generating a random location on the board
        Random random = new Random();


        while(CheckOnTopOfOtherEntity(left, top) || top == 0) //While the new fruit's position is on another entity (or is not yet set)
        {
            //Get a random position within the space of the board
            left = random.Next(Left+1, Left+Width);
            top = random.Next(Top+1, Top+Height);
        }

        //Create a new fruit with the random position and add it to the list of fruits
        _fruits.Add(new Fruit(left, top)); 

    }
    private bool CheckOnTopOfOtherEntity(int left, int top) //Checks if the given left and top are on top of another entity
    {   
        //Check for an overlap using linq (super fancy)
        bool result = Entities.Any(entity => entity.Top == top && entity.Left == left); 
        return result;
    }
    private void GetInput()
    {
        //Gets user input while the game is running
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

        //Begins to restart the game once the game thread is dead (meaning the game is over)
        EraseLoseScreen(); //Erase the lose screen here so that the last console readkey blocks on this thread instead of on the game thread (that would cause two readkeys)
        Restart();
    }
    public void Restart() //Erases all the previous entities and creates new ones
    {
        //Erase every entity on the board
        EraseAllEntities();

        //Make a new list of fruits and a new worm
        _fruits = new List<Fruit>();
        _theWorm = new Worm(Left+Width/2, Top+Height/2, _defaultWormLength);

        //Start the game again
        StartGame();
    }
    private void EraseFruits(){foreach(Fruit fruit in _fruits) fruit.EraseEntity();} //Erases all the fruits on the board
    private void EraseAllEntities() //Erases all the entities on the board
    {
        foreach(Entity entity in Entities) entity.EraseEntity();
    }
    private int DelayBasedOnWormLength() //Calculates the delay to be used per "worm advance". It is based on the worms current length
    {
        int delay = 200 - _theWorm.Length;
        if(delay<100) return 100;
        return delay;
    }
}