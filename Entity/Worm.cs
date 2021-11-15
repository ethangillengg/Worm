using System;
using System.Linq;
using System.Collections.Generic;

public class Worm
{
/*A worm which is controlled by user inputs and may move around the console
 *RESPONSIBILITES:
 * - Stores all the segments of the worm with their positions and colors
 * - Can display the worm on the console
 * - Can advance the worm forward based on user inputs
*/
    public int Top{get => _head.Top;} //Return distance from the head of the worm to the top of the console
    public int Left{get => _head.Left;} //Return distance from the head of the worm to the left of the console
    public WormSegment Head{get => new WormSegment(_head);} //Return a copy of the head of the worm
    private WormSegment _head{get => _segments[0];} //Return the head of the worm
    public WormSegment Tail{get => new WormSegment(_tail);} //Return a copy of the tail of the worm
    private WormSegment _tail{get => _segments[Length-1];} //Return the tail of the worm
    public int Length{
        get => _segments.Count; //Return the number of worm segments
        set
        {
            if(value<1) return; //If trying to set worm length to less than 1, do nothing
            while(Length<value) //Create new segments until the worm is the desired length
            {
                _segments.Add(new WormSegment(_tail.Left, _tail.Top));
            }
            while(Length > value) //Remove segments until the worm is the desired length
            {
                _segments[Length-1].EraseEntity();
                _segments.RemoveAt(Length-1);
            }
        }
    }
    public IList<WormSegment> Segments //Returns a readonly list of the worm's segments
    {
        get => _segments.AsReadOnly();
    }
    private List<WormSegment> _segments = new List<WormSegment>(); //List of the segments in the worm
    public ConsoleKey Input{get; set;} = ConsoleKey.A; //Stores the last input made to the worm (for controlling movement)
    public static readonly ConsoleColor DefaultHeadColor = ConsoleColor.Red; //Default color for the head of the worm
    
    public Worm(int left, int top, int length = 5)
    {
        while(--length >= 0)
        {
            _segments.Add(new WormSegment(++left, top)); //Adds a new segment to the left of the last one until the worm is the desired length
        }
        _head.Color = DefaultHeadColor; //Sets the color of the worm head to the default color of a worm head
        _head.PrintEntity(); //Reprint the head with its new color
    }

    public void Advance() //Advances the worm forward in the direction of the last input made
    {
        MoveHeadForward(); //First insert a new head one tile forward in the direction of the last input to the list of segments
        _tail.EraseEntity(); //Erase the tail of the worm
        _segments.Remove(_tail); //Remove the tail from the list

        //There is a 
    }
    private void MoveHeadForward() //Creates a new head one tile forward in the direction of the last input
    {
        //First set the old head's color to be the same as the rest of the body
        _head.Color = _segments[1].Color;

        //Reprint the old head with its new color (same color as the body)
        _head.PrintEntity();

        //Next, insert a new head one tile forward
        if(Input == ConsoleKey.W) 
        {
            InsertNewHead(Left, Top-1);
        }else if(Input == ConsoleKey.A)
        {
            InsertNewHead(Left-1, Top);
        }else if(Input == ConsoleKey.S)
        {
            InsertNewHead(Left, Top+1);
        }else if(Input == ConsoleKey.D)
        {
            InsertNewHead(Left+1, Top);
        }
    }
    private void InsertNewHead(int left, int top) //Creates a new head and inserts it to index [0] of the segments
    {
        var newHead = new WormSegment(left, top); //Create the new head at the specified position
        newHead.Color = DefaultHeadColor; //Set the head's color
        newHead.PrintEntity(); //Print the new head since its color was updated
        _segments.Insert(0, newHead); //Insert the head to the list
    }
    public void EraseWorm(){foreach(WormSegment seg in _segments) seg.EraseEntity();} //Erases all the segments of the worm
}