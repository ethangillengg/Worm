public struct Position
{
/*A position on the console
 *RESPONSIBILITES:
 * - Stores the distance from the left of the console
 * - Stores the distance from the top of the console
*/
    public int Left { get; set; }
    public int Top { get; set; }
    public Position(int left, int top)
    {
        Left = left;
        Top = top;
    }

    public Position(Position pos)
    {
        Left = pos.Left;
        Top = pos.Top;
    }
}