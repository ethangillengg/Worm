using System;
public delegate void Update();

public class ConsoleVariable <T>: ConsoleVariable, IComparable<ConsoleVariable<T>>
where T : IComparable<T>{ //figure out how to get format to work with this
    public event Update EraseLine;
    public event Update DrawLine;
    public T Value{
        get => _value;
        set{
            this.EraseLine?.Invoke();
            _value = value;
            this.DrawLine?.Invoke();
        }
    }
    
    private T _value;

    object ConsoleVariable.Value {
        get {
            return _value;
        }
    }
    string ConsoleVariable.String {
        get {
            return _value.ToString();
        }
    }
    public ConsoleVariable(T value)
    {
        _value = value;
    }
    public int CompareTo(ConsoleVariable<T> other){
        if(other == null) return -1;
        return Value.CompareTo(other.Value);
    }

}
public interface ConsoleVariable{
    public string String{ get; }
    public object Value{ get; }
    public event Update EraseLine;
    public event Update DrawLine;
}