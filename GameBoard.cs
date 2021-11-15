using System;

public class GameBoard
{
/*A game board for the worm game
 *RESPONSIBILITES:
 * - Stores the position and size of the board
 * - Can print the board on the console
*/
    protected int Top; //Distance from the corner of the board to the top of the console
    protected int Left; //Distance from the corner of the board to the left of the console
    protected int Height; //Height of the board
    protected int Width; //Width of the board

    public GameBoard(int left = 10, int top = 3, int width = 30, int height = 20)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
        PrintBoard();
    }
    public void PrintBoard()
    {
        string topBorder = " ";
        for(int i = 1; i < Width; i++) topBorder+="-";

        //Draw the top border of the board
        Console.SetCursorPosition(Left, Top);
        Console.Write(topBorder); 

        //Draw the bottom border of the board
        Console.SetCursorPosition(Left, Top+Height);
        Console.Write(topBorder);

        for(int i = 1; i < Height; i++)
        {
            //Draw the Left border of the board
            Console.SetCursorPosition(Left, Top+i);
            Console.Write("|");

            //Draw the right border of the board
            Console.SetCursorPosition(Left+Width, Top+i);
            Console.Write("|");
        }
    }
}