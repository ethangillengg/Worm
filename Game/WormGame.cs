using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
public class WormGame : WormGameBoard
{
/*The worm game
 *RESPONSIBILITES:
 * - Can start and restart a game
 * - Defines behavior for how a game works (how many fruit to generate, when to increment length of the worm, etc.)
*/
    private static readonly int _defaultWormLength = 5; //Default length of the worm
    private static readonly Tuple<int, int> _numberOfFruit = new Tuple<int, int>(3,10); //The minimum and maximum number of fruit to be generated on the board

    private Thread _gameThread; //A thread to run the game (the main thread is used for user inputs)

    public WormGame(int left = 10, int top = 3, int width = 30, int height = 20):
    base(left, top, width, height)
    {
        //Make a worm that is placed in the middle of the board with a default length
        _theWorm = new Worm(_left+_width/2, _top+_height/2, _defaultWormLength); 
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
        PrintCountdown(); 

        //Generate the fruits on the screen, and reprint the board
        GenerateFruits();
        PrintBoard();

        //Start the game
        _gameThread.Start(); //The thread running the game starts here
        GetInput(); //The main thread is sent to go get user inputs
    }
    
    private void RunGame() //Advances the worm, checks for fruit eaten/game over, recalculates the delay, and then waits until the next turn
    {
        int delay = CalcDelay(); //The delay per turn in milliseconds
        while(true) //Run until the game is over (then it uses return to get out of the while loop)
        {
            Console.CursorVisible = false; //Make the cursor invisible (sometimes it goes visible when the window size of the terminal is changed)
            _theWorm.Advance(); //Advance the worm one tile

            if(CheckIfAteFruit())
            {
                //If fruit was eaten, increment the worm's length and 
                //reprint that length to the top of the screen
                _theWorm.Length+=3;
                PrintWormLength();

                //Recalculate the delay that the thread waits every turn
                //since it is based on the length of the worm
                delay = CalcDelay();
            }else if(CheckIfGameOver())
            {
                //If the game is over, print a lose screen and end the thread by returning the function
                PrintLoseScreen();
                return;
            }
            //Wait until the next turn
            Thread.Sleep(delay);
        }
    }

    private bool CheckIfGameOver() //Checks if the worm's head collided with a border of the game board or with one of it's own segments
    {
        //Check if the worm ran into a wall
        if(_theWorm.Left <= _left) return true;
        if(_theWorm.Top <= _top) return true;
        if(_theWorm.Left >= _left+_width) return true;
        if(_theWorm.Top >= _top+_height) return true;

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
            left = random.Next(_left+1, _left+_width);
            top = random.Next(_top+1, _top+_height);
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
        _theWorm = new Worm(_left+_width/2, _top+_height/2, _defaultWormLength);

        //Start the game again
        StartGame();
    }
    private int CalcDelay() //Calculates the delay to be used per "worm advance". It is based on the worms current length
    {
        int delay = 150 - _theWorm.Length/2;
        if(delay<75) return 75;
        return delay;
    }
}