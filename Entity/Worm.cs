using System;
using System.Collections.Generic;
public class Worm : WormSegment
{
    public int Length{
        get => _segments.Count + 1;
        set
        {
            if(value<1) return;
            while(Length<value)
            {
                // Console.ResetColor();
                _segments.Add(new WormSegment(
                    _segments[Length-2].Left,
                    _segments[Length-2].Top));
            }
            while(Length > value)
            {
                _segments[Length-2].Erase();
                _segments.RemoveAt(Length-2);
            }
        }
    }
    private List<WormSegment> _segments; 
    public ConsoleKey Input{get; set;} = ConsoleKey.A; 
    public Worm(int left, int top, int length):base(left,top)
    {
        _color = ConsoleColor.Red;
        Draw();

        _segments = new List<WormSegment>();
        while(--length > 0)
        {
            _segments.Add(new WormSegment( ++left,_top));
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
            temp = new Tuple<int, int>(_segments[i].Left, _segments[i].Top);
            (_segments[i].Left, _segments[i].Top) = prevSegPos;
            prevSegPos = temp;
        }
    }


}