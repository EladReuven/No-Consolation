using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public static class Menu
    {
        public static ConsoleColor playerColor = ConsoleColor.White;
        public static ConsoleColor enemyColor = ConsoleColor.White;



        public static void GameMenu()
        {
            string choice;
            bool inMenu = true;
            while(inMenu)
            {
                Console.Clear();
                Console.WriteLine("MENU");
                Console.WriteLine("1. Edit player\n" +
                    "2. Edit enemy\n" +
                    "3. Change difficulty\n" +
                    "4. View full Log\n" +
                    "5. Exit");
                choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        //color or symbol
                        Menu.PlayerOptions();
                        break;
                    case "2":
                        //color or symbol
                        Menu.EnemyOptions();
                        break;
                    case "3":
                        // difficulty picker
                        Menu.ChooseDifficulty();
                        break;
                    case "4":
                        Console.Clear();
                        LogHandler.PrintLongLog();
                        Console.WriteLine("Press anything to continue");
                        Console.ReadKey();
                        break;
                    case "5":
                        inMenu = false;
                        Console.Clear();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

        }

        public static void ChangeSymbol(Level.symbolEnum symbol)
        {
            char newSymbol;
            Console.WriteLine("Current symbol: " + Level.mapSymbols[symbol]);
            Console.WriteLine("Enter a new symbol: ");
            newSymbol = char.Parse(Console.ReadLine());
            Level.mapSymbols[symbol] = newSymbol;
        }

        public static void ChangePlayerColor()
        {
            Console.Clear();
            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.ForegroundColor = cc;
                Console.WriteLine($"{cc} = {(int)cc}");
            }

            int color = int.Parse(Console.ReadLine());
            playerColor = (ConsoleColor)color;
        }

        public static void ChangeEnemyColor()
        {
            Console.Clear();
            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.ForegroundColor = cc;
                Console.WriteLine($"{cc} = {(int)cc}");
            }

            int color = int.Parse(Console.ReadLine());
            enemyColor = (ConsoleColor)color;
        }

        public static void PlayerOptions()
        {
            string choice;
            Console.WriteLine("1. Change Color\n2. Change Symbol");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Menu.ChangePlayerColor();
                    break;
                case "2":
                    char newSymbol;
                    Console.WriteLine("Current symbol: " + Player.playerSymbol);
                    Console.WriteLine("Enter a new symbol: ");
                    newSymbol = char.Parse(Console.ReadLine());
                    Player.playerSymbol = newSymbol;
                    break;
            }
        }

        public static void EnemyOptions()
        {
            string choice;
            Console.WriteLine("1. Change Color\n2. Change Symbol");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Menu.ChangeEnemyColor();
                    break;
                case "2":
                    Menu.ChangeSymbol(Level.symbolEnum.enemySymbol);
                    break;
            }

        }

        public static string DifficultyToWords()
        {
            switch(Level.progressionDifficulty)
            {
                case 1:
                    return "Current difficulty: Normal";
                case 2:
                    return "Current difficulty: Hard";
                default:
                    return "";
            }
        }

        public static void ChooseDifficulty()
        {
            bool boolLoop = true;
            while(boolLoop)
            {
                Console.Clear();
                Console.WriteLine("Choose a difficulty");
                Console.WriteLine("1. Normal\n2. Hard (enemies difficulty will increase twice as fast)");
                string choice = Console.ReadLine();
                if(choice == "1" || choice == "2")
                {
                    Level.progressionDifficulty = int.Parse(choice);
                    boolLoop = false;
                }
                else
                {
                    Console.WriteLine("please choose 1 or 2");
                }
            }
        }
    }
}
