using System;
public class Fruit : Entity
{
    public override string Body{get => "*";} //Set "*" as the default body of all fruit entities
    public override ConsoleColor Color{get => ConsoleColor.Green;} //Set default color of fruit to be green
    public Fruit(int left, int top):base(left, top) { }
    public Fruit(Position pos):base(pos) { }
}