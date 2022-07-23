using System;
using No_Consolation;

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
        Console.Clear();
        Console.WriteLine("GAME OVER");
        UtilityMethods.YouDied();
    }
}