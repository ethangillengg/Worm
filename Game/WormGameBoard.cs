using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
public class WormGameBoard : GameBoard
{
    /*A game board for the worm game
 *RESPONSIBILITES:
 * - Stores the entities used in the game
 * - Can print and erase all entities  used in the worm game
 * - Defines default lose and welcome messages for the worm game
*/
    public override string WelcomeMessage{get => "Welcome to Worm, press any key to start!";} //A default welcome message for the worm game
    public override string LoseMessage{get => "YOU LOSE!!";} //A default lose message for the worm game
    public IList<Entity> Entities //A readonly list of all the entities currently in the game
    {
        get
        {
            var result = new List<Entity>();
            result = result.Concat(_fruits).Concat(_theWorm.Segments).ToList();
            return result.AsReadOnly();
        }
    }
    protected List<Fruit> _fruits = new List<Fruit>(); //A list of all the fruits in the game
    protected Worm _theWorm; //The worm used in the game
    public WormGameBoard(int left = 10, int top = 3, int width = 30, int height = 20):
    base(left, top, width, height){ }

    protected override void PrintLoseScreen() //Prints a lose screen
    {
        base.PrintLoseScreen();
        Console.SetCursorPosition(_left+_width/4, _top+_height/2-2);
        Console.Write("Press any key to restart");
    }
    protected override void EraseLoseScreen() //Erases the lose screen
    {
        base.EraseLoseScreen();
        Console.SetCursorPosition(_left+_width/4, _top+_height/2-2);
        Console.Write("".PadRight(50));
    }
    protected void EraseFruits(){foreach(Fruit fruit in _fruits) fruit.EraseEntity();} //Erases all the fruits on the board
    protected void EraseAllEntities() //Erases all the entities on the board
    {
        foreach(Entity entity in Entities) entity.EraseEntity();
    }
    protected void PrintCountdown() //Prints out a countdown above the worm signalling that the game is starting
    {
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("3...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("2...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("1...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("Go!   ");
        Thread.Sleep(1000);
        EraseWelcomeMessage();
        Console.SetCursorPosition(_theWorm.Left, _theWorm.Top-1);
        Console.Write("".PadRight(6));
    }
    protected void PrintWormLength() //Prints the current worm's length
    {
        Console.SetCursorPosition(_left+1, _top-1);
        Console.Write("Worm length: {0}".PadRight(20), _theWorm.Length);
    }
}