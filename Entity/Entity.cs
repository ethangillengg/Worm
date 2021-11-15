using System;
public abstract class Entity
{
    public Position Pos
    { 
        get => _pos; 
        set => _pos = value;
    }  
    private Position _pos = new Position(0,0); //Position of the entity on the Console
    public int Top{get => _pos.Top;}
    public int Left{get => _pos.Left;}
    public virtual ConsoleColor Color //Allow color to be changed
    {
        get => _color;
        set => _color = value;
    } 
    private ConsoleColor _color = ConsoleColor.White; //Set default color for all entities to be white

    public virtual string Body{get => _body;}
    private string _body; //Content of the entity (string to be printed to the console)

    public Entity(string entity = "", int left = 0, int top = 0)
    {
        _body = entity;
        _pos.Left = left;
        _pos.Top = top;
        PrintEntity();
    }
    public Entity(string entity, Position pos): this(entity, pos.Left, pos.Top){ }
    public Entity(int left, int top): this("", left, top){ }
    public Entity(Position pos): this("", pos){ }

    public void PrintEntity() //Method to print the entity
    {
        Console.SetCursorPosition(Pos.Left, Pos.Top); 
        Console.ForegroundColor = Color;
        Console.Write(Body);
        Console.ResetColor(); //Reset the color of the console print
    }

    public void EraseEntity() //Method to erase the entity
    {
        Console.SetCursorPosition(Pos.Left, Pos.Top);
        Console.Write("".PadRight(Body.Length));
    }
}