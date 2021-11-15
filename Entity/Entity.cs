using System;

public abstract class Entity
{
/*An abstract class for an entity on the console
 *RESPONSIBILITES:
 * - Keeps track of position and color of the entity
 * - Can print the entity to the console
 * - Can erase te entity from the console
*/
    public int Top{get; set;}
    public int Left{get; set;}
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
        Left = left;
        Top = top;
        PrintEntity();
    }
    public Entity(int left, int top): this("", left, top){ }
    public void PrintEntity() //Method to print the entity
    {
        Console.SetCursorPosition(Left, Top); 
        Console.ForegroundColor = Color;
        Console.Write(Body);
        Console.ResetColor(); //Reset the color of the console print
    }

    public void EraseEntity() //Method to erase the entity
    {
        Console.SetCursorPosition(Left, Top);
        Console.Write("".PadRight(Body.Length));
    }
}