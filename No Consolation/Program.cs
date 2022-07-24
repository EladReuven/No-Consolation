using System;
using No_Consolation;

public class Program
{


    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Game game = new Game();
        game.Start();
        Console.Clear();
        Console.WriteLine("GAME OVER");
        UtilityMethods.YouDied();
        BeepMusic.MarioDeath();
    }
}