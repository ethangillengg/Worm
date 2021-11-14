using System;
public class GameBoard
{
    protected int Top;
    protected int Left;
    protected int Height;
    protected int Width;

    public GameBoard(int left, int top, int height)
    {
        Top = top;
        Left = left;
        Height = height;
        Width = height*2;
        DrawBoard();
    }
    public void DrawBoard()
    {


        string topBorder = " ";
        for(int i = 1; i < Width; i++) topBorder+="-";

        Console.SetCursorPosition(Left, Top);
        Console.Write(topBorder);

        Console.SetCursorPosition(Left, Top+Height);
        Console.Write(topBorder);

        for(int i = 1; i < Height; i++)
        {
            Console.SetCursorPosition(Left, Top+i);
            Console.Write("|");

            Console.SetCursorPosition(Left+Width, Top+i);
            Console.Write("|");
        }
    }
}