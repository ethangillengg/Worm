using System;
public class GameBoard
{
    private int _top;
    private int _left;
    private int _height;
    private int _width;

    public GameBoard(int left, int top, int height)
    {
        _top = top;
        _left = left;
        _height = height;
        _width = height*2;

        DrawBoard();
    }
    public void DrawBoard()
    {
        string topBorder = " ";
        for(int i = 1; i < _width; i++) topBorder+="-";

        Console.SetCursorPosition(_left, _top);
        Console.Write(topBorder);

        Console.SetCursorPosition(_left, _top+_height);
        Console.Write(topBorder);

        for(int i = 1; i < _height; i++)
        {
            Console.SetCursorPosition(_left, _top+i);
            Console.Write("|");

            Console.SetCursorPosition(_left+_width, _top+i);
            Console.Write("|");
        }

    }
}