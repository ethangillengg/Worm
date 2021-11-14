using System;
public class Fruit : Entity
{
    public Fruit(int leftBorder, int topBorder, int height):
    base("*")
    {
        (_left, _top) = getPosInBorder(leftBorder, topBorder, height);
        _color = System.ConsoleColor.Green;
        Draw();
    }
    private Tuple<int,int> getPosInBorder(int leftBorder, int topBorder, int height)
    {
        Random random = new Random();
        int left = random.Next(leftBorder+1, leftBorder+height*2);
        int top = random.Next(topBorder+1, topBorder+height);
        return new Tuple<int, int>(left, top);
    }
}