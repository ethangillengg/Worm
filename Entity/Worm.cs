using System;
using System.Collections.Generic;
public class Worm
{
    public int Left{get => Segments[0].Left;}
    public int Top{get => Segments[0].Top;}

    public int Length{
        get => Segments.Count;
        set
        {
            if(value<1) return;
            while(Length<value)
            {
                Segments.Add(new WormSegment(
                    Segments[Length-1].Left,
                    Segments[Length-1].Top));
            }
            while(Length > value)
            {
                Segments[Length-1].Erase();
                Segments.RemoveAt(Length-1);
            }
        }
    }
    public List<WormSegment> Segments; 
    public ConsoleKey Input{get; set;} = ConsoleKey.A; 
    public Worm(int left, int top, int length)
    {

        Segments = new List<WormSegment>();
        while(--length >= 0)
        {
            Segments.Add(new WormSegment( ++left, top));
        }
        Segments[0].Color = ConsoleColor.Red;
    }

    public void Advance()
    {
        Segments[0].Color = ConsoleColor.White;
        if(Input == ConsoleKey.W)
        {
            Segments.Insert(0, CreateHead(Left, Top-1));
        }else if(Input == ConsoleKey.A)
        {
            Segments.Insert(0, CreateHead(Left-1, Top));
        }else if(Input == ConsoleKey.S)
        {
            Segments.Insert(0, CreateHead(Left, Top+1));
        }else if(Input == ConsoleKey.D)
        {
            Segments.Insert(0, CreateHead(Left+1, Top));
        }
        Segments[Length-1].Erase();
        Segments.RemoveAt(Length-1);
    }
    private WormSegment CreateHead(int left, int top)
    {
        var seg = new WormSegment(left, top);
        seg.Color = ConsoleColor.Red;
        return seg;
    }
    public void EraseWorm()
    {
        Console.SetCursorPosition(Segments[0].Left,Segments[0].Top);
        System.Console.Write(" ");

        foreach(WormSegment seg in Segments)
        {
            Console.SetCursorPosition(seg.Left,seg.Top);
            System.Console.Write(" ");
        }
    }


}