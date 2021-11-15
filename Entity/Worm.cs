using System;
using System.Collections.Generic;
public class Worm
{
    public Position Pos{get => Segments[0].Pos;} //Return position of the head of the worm
    public int Top{get => Pos.Top;} //Return Top of the head of the worm
    public int Left{get => Pos.Left;} //Return Left of the head of the worm
    public WormSegment Head{get => Segments[0];}
    public WormSegment Tail{get => Segments[Length-1];}
    public int Length{
        get => Segments.Count; //Return the number of worm segments
        set
        {
            if(value<1) return; //If trying to set worm length to less than 1, do nothing
            while(Length<value) //Create new segments until the worm is the desired length
            {
                Segments.Add(new WormSegment(Segments[Length-1].Pos));
            }
            while(Length > value) //Remove segments until the worm is the desired length
            {
                Segments[Length-1].EraseEntity();
                Segments.RemoveAt(Length-1);
            }
        }
    }
    public List<WormSegment> Segments = new List<WormSegment>(); //List of the segments in the worm
    public ConsoleKey Input{get; set;} = ConsoleKey.A; //Stores the last input made to the worm (for controlling movement)
    public static readonly ConsoleColor DefaultHeadColor = ConsoleColor.Red; //Default color for the head of the worm
    public Worm(int left, int top, int length = 5)
    {
        while(--length >= 0)
        {
            Segments.Add(new WormSegment(++left, top)); //Adds a new segment to the left of the last one until the worm is the desired length
        }
        Head.Color = DefaultHeadColor; //Sets the color of the worm head to the default color of a worm head
        Head.PrintEntity(); //Reprint the head with its new color
    }

    public void Advance() //Advances the worm forward in the direction of the last input made
    {
        MoveHeadForward(); //First insert a new head one tile forward in the direction of the last input to the list of segments
        Tail.EraseEntity(); //Erase the tail of the worm
        Segments.Remove(Tail); //Remove the tail from the list

        //There is a 
    }
    private void MoveHeadForward() //Creates a new head one tile forward in the direction of the last input
    {
        //First set the old head's color to be the same as the rest of the body
        Head.Color = Segments[1].Color;

        //Reprint the old head with its new color (same color as the body)
        Head.PrintEntity();

        //Next, insert a new head one tile forward
        if(Input == ConsoleKey.W) 
        {
            InsertNewHead(Pos.Left, Pos.Top-1);
        }else if(Input == ConsoleKey.A)
        {
            InsertNewHead(Pos.Left-1, Pos.Top);
        }else if(Input == ConsoleKey.S)
        {
            InsertNewHead(Pos.Left, Pos.Top+1);
        }else if(Input == ConsoleKey.D)
        {
            InsertNewHead(Pos.Left+1, Pos.Top);
        }
    }
    private void InsertNewHead(int left, int top) //Creates a new head and inserts it to index [0] of the segments
    {
        var newHead = new WormSegment(left, top); //Create the new head at the specified position
        newHead.Color = DefaultHeadColor; //Set the head's color
        newHead.PrintEntity(); //Print the new head since its color was updated
        Segments.Insert(0, newHead); //Insert the head to the list
    }
    public void EraseWorm() //Erases all the segments of the worm
    {
        foreach(WormSegment seg in Segments) seg.EraseEntity();
    }


}