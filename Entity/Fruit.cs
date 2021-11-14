using System;
public class Fruit : Entity
{
    public Fruit(int left, int top):
    base("*")
    {
        _left = left;
        _top = top;
        _color = System.ConsoleColor.Green;
        Draw();
    }
}