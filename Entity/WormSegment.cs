using System;
public class WormSegment : Entity
{
    public override string Body{get => "O";} //Set "O" as the default body of all worm segment entities
    public override ConsoleColor Color{ //Allow changes to the color fo a segment
        get => _color;
        set => _color = value;
    } 
    private ConsoleColor _color = ConsoleColor.White; //Set default color of worm segments to be white

    public WormSegment(int left, int top):base(left, top) { }
    public WormSegment(Position pos):base(pos) { }
    
}