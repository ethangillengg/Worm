public struct Position //basic structure for position of elements on the console
{
    public int Left { get; set; }
    public int Top { get; set; }
    public Position(int left, int top)
    {
        Left = left;
        Top = top;
    }
}