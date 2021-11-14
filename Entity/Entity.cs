using System;
public class Entity
{
    public ConsoleColor Color
    {
        set
        {
            _color = value;
            Draw();
        }
    }
    protected ConsoleColor _color = ConsoleColor.White;
    private string _entity;
    public int Top{
        get => _top;
        set
        {
            // Erase();
            _top = value;
            // Draw();
        }
    }
    protected int _top;
    public int Left{
        get => _left;
        set
        {
            // Erase();
            _left = value;
            // Draw();
        }
    }
    protected int _left;

    public Entity(string entity, int left = 0, int top = 0)
    {
        _entity = entity;
        _top = top;
        _left = left;
    }

    protected void Draw()
    {
        Console.SetCursorPosition(Left, Top);
        Console.ForegroundColor = _color;
        Console.Write(_entity);
        Console.ResetColor();
    }

    public void Erase()
    {
        Console.SetCursorPosition(Left, Top);
        Console.Write("".PadRight(_entity.Length));
    }
}