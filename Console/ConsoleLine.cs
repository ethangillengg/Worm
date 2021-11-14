using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class ConsoleLineOld{
    public string Name{get; init;}
    public string LineFormat{get; set;}
    public ArrayList Insertions = new ArrayList();
    public int Length{get {return formatLine().Length;}}
    public ConsoleLineOld(string _name, string _lineFormat, ArrayList _insertions){
        Name = _name;
        LineFormat = _lineFormat;
        foreach(object e in _insertions) Insertions.Add(e);
    }
    public ConsoleLineOld(string _name, string _lineFormat):
    this(_name, _lineFormat, new ArrayList()){ }

    public string formatLine(){
        return LineFormat.Contains("{0}")   ? String.Format(LineFormat, Insertions.ToArray())
                                            : LineFormat;
    }

}

public class ConsoleLine{
    public string Append{
        get => _append;
        set{
            Erase();
            _append = value;
            Display();
        }
    }
    private string _append;
    public ConsoleColor BackgroundColor{
        get => _backgroundColor;
        set{
            Erase();
            _backgroundColor = value;
            Display();
        }
    } 
    private ConsoleColor _backgroundColor = Console.BackgroundColor;
    public ConsoleColor ForegroundColor{
        get => _foregroundColor;
        set{
            Erase();
            _foregroundColor = value;
            Display();
        }
    } 
    private ConsoleColor _foregroundColor = Console.ForegroundColor;
    public bool Visible{
        get => _visible;
        set{
            Erase();
            _visible = value;
            Display();
        }
    }
    private bool _visible = false;

    public int Length{get => Line.Length;}
    public string Line{
        get
        {
        return Format.Contains("{0}")   
        ? String.Format(Format, _variables.Select(var => var.Value).ToArray())
        : Format;
        }
    }
    public string Format{
        get => _format + _append; 
        init => _format = value;
    } //Formatted string for the line
    private string _format;
    public int Top{
        get => _top; 
        set
        {
            // if(value<0 || value>Console.BufferHeight) return;
            Erase();
            _top = value;
            Display();
        }
    }
    private int _top;
    public int Left{
        get => _left; 
        set
        {
            // if(value<0 || value>Console.BufferWidth) return;
            Erase();
            _left = value;
            Display();
        }
    }
    private int _left;
    private List<ConsoleVariable> _variables;

    public ConsoleLine(string lineFormat, int left, int top, List<ConsoleVariable> variables)
    {
        _top = top;
        _left = left;
        Format = lineFormat;
        _variables = variables;
        foreach(ConsoleVariable variable in _variables){
            variable.EraseLine+=this.Erase;
            variable.DrawLine+=this.Display;
        }
        Display();
    }
    public ConsoleLine(string lineFormat, List<ConsoleVariable> variables){
        _top = 0;
        _left = 0;
        Format = lineFormat;
        _variables = variables;
        foreach(ConsoleVariable variable in _variables){
            variable.EraseLine+=this.Erase;
            variable.DrawLine+=this.Display;
        }
        Display();
    }
    public ConsoleLine(string lineFormat, int left = 0, int top = 0)
    {
        _top = top;
        _left = left;
        Format = lineFormat;
        Display();
    }
    
    private void Display()
    {
        lock(_locker){
            if(!_visible) return;

            Console.ForegroundColor = _foregroundColor;
            Console.BackgroundColor = _backgroundColor;

            Console.SetCursorPosition(Left, Top);
            Console.Write(Line);

            Console.ResetColor();
        }
    }
    private void Erase()
    {
        lock(_locker){
            if(!_visible) return;
            Console.SetCursorPosition(Left, Top);
            Console.Write("".PadRight(Length));
        }
    }
    public static object _locker = new object();
}
