using System;
public class WormSegment : Entity
{
/*A worm segment which is a type of entity
 *RESPONSIBILITES:
 * - Sets the default body & color of a worm segment entity
 * - Allows the color of a worm segment to be changed
*/
    public override string Body{get => "鬱";} //Set "O" as the default body of all worm segment entities
    public override ConsoleColor Color{ //Allow changes to the color fo a segment
        get => _color;
        set => _color = value;
    } 
    private ConsoleColor _color = ConsoleColor.White; //Set default color of worm segments to be white
    public WormSegment(WormSegment seg)
    {
        Left = seg.Left;
        Color = seg.Color;
    }
    public WormSegment(int left, int top):base(left, top) { }
}