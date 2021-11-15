using System;

public class Fruit : Entity
{
/*A fruit which is a type of entity
 *RESPONSIBILITES:
 * - Sets the default body & color of a fruit entity
*/
    public override string Body{get => "*";} //Set "*" as the default body of all fruit entities
    public override ConsoleColor Color{get => ConsoleColor.Green;} //Set default color of fruit to be green
    public Fruit(int left, int top):base(left, top) { }
}