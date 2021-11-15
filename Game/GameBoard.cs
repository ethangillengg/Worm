using System;

public class GameBoard
{
/*A game board for any game
 *RESPONSIBILITES:
 * - Stores the position and size of the board
 * - Can print the board, welcome message and lose message on the console
 * - Can erase the board, welcome message and lose message off of the console
*/
    protected int _top; //Distance from the corner of the board to the top of the console
    protected int _left; //Distance from the corner of the board to the left of the console
    protected int _height; //Height of the board
    protected int _width; //Width of the board
    public virtual string WelcomeMessage //A default welcome message for the game
    {
        get => "Welcome!";
    }
    public virtual string LoseMessage //A default message when the game is lost
    {
        get => "You lost...";
    }

    public GameBoard(int left = 10, int top = 3, int width = 30, int height = 20)
    {
        _left = left;
        _top = top;
        _width = width;
        _height = height;
        PrintBoard();
    }
    protected virtual void PrintBoard() //Prints the game board's borders
    {
        string topBorder = " ";
        for(int i = 1; i < _width; i++) topBorder+="-";

        //Draw the top border of the board
        Console.SetCursorPosition(_left, _top);
        Console.Write(topBorder); 

        //Draw the bottom border of the board
        Console.SetCursorPosition(_left, _top+_height);
        Console.Write(topBorder);

        for(int i = 1; i < _height; i++)
        {
            //Draw the Left border of the board
            Console.SetCursorPosition(_left, _top+i);
            Console.Write("|");

            //Draw the right border of the board
            Console.SetCursorPosition(_left+_width, _top+i);
            Console.Write("|");
        }
    }
    protected virtual void EraseBoard()
    {
        //Erase the top border of the board
        Console.SetCursorPosition(_left, _top);
        Console.Write("".PadRight(_width));

        //Erase the bottom border of the board
        Console.SetCursorPosition(_left, _top+_height);
        Console.Write("".PadRight(_width));

    for(int i = 1; i < _height; i++)
        {
            //Erase the Left border of the board
            Console.SetCursorPosition(_left, _top+i);
            Console.Write(" ");

            //Erase the right border of the board
            Console.SetCursorPosition(_left+_width, _top+i);
            Console.Write(" ");
        }
    }
    protected virtual void PrintWelcomeMessage() //Prints a message welcoming the user
    {
        Console.SetCursorPosition(_left+_width/8, _top+_height/2-3);
        Console.Write(WelcomeMessage);
    }
    protected virtual void EraseWelcomeMessage()
    {
        Console.SetCursorPosition(_left+_width/8, _top+_height/2-3);
        Console.Write("".PadRight(50));
    }
    protected virtual void PrintLoseScreen() //Prints a lose screen
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(_left+_width/4, _top+_height/2-3);
        Console.Write(LoseMessage);
        Console.ResetColor();
    }
    protected virtual void EraseLoseScreen() //Erases the lose screen
    {
        Console.SetCursorPosition(_left+_width/4, _top+_height/2-3);
        Console.Write("".PadRight(50));
    }
}