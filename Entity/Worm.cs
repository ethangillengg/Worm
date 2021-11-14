using System;
using System.Collections.Generic;
public class Worm : WormSegment
{
    public int Length{
        get => Segments.Count + 1;
        set
        {
            if(value<1) return;
            while(Length<value)
            {
                Segments.Add(new WormSegment(
                    Segments[Length-2].Left,
                    Segments[Length-2].Top));
            }
            while(Length > value)
            {
                Segments[Length-2].Erase();
                Segments.RemoveAt(Length-2);
            }
        }
    }
    public List<WormSegment> Segments; 
    public ConsoleKey Input{get; set;} = ConsoleKey.A; 
    public Worm(int left, int top, int length):base(left,top)
    {
        _color = ConsoleColor.Red;
        Draw();

        Segments = new List<WormSegment>();
        while(--length > 0)
        {
            Segments.Add(new WormSegment( ++left,_top));
        }
    }

    public void Advance()
    {
        var prevSegPos = new Tuple<int,int>(Left, Top);
        var temp = new Tuple<int,int>(0,0);
        if(Input == ConsoleKey.A)
        {
            Left--;
        }else if(Input == ConsoleKey.D)
        {
            Left++;
        }else if(Input == ConsoleKey.W)
        {
            Top--;
        }else if(Input == ConsoleKey.S)
        {
            Top++;
        }

        for(int i = 0; i < Length-1; i++)
        {
            temp = new Tuple<int, int>(Segments[i].Left, Segments[i].Top);
            (Segments[i].Left, Segments[i].Top) = prevSegPos;
            prevSegPos = temp;
        }
    }
    public void EraseWorm()
    {
        Console.SetCursorPosition(Left,Top);
        System.Console.Write(" ");

        foreach(WormSegment seg in Segments)
        {
            Console.SetCursorPosition(seg.Left,seg.Top);
            System.Console.Write(" ");
        }
    }


}