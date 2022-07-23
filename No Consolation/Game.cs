using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class Game
    {
        Level newLevel;
        Random random = new Random();
        

        public void Play()
        {
            Console.WriteLine("What is your name challenger?");
            string userName = Console.ReadLine();
            Player player = new Player(userName, 10, 2, 3);
            Interactions interactions = new Interactions();

            LogHandler.Add("Game Start");

            //endless levels loop (with enemy for now)
            while (!player.playerCP.IsDead())
            {

                Console.Clear();
                newLevel = new Level();
                EveryNewRoom(player);

                newLevel.SetPlayer(player);
                interactions.ConnectComponents(player, newLevel);
                if (Level.ShopCounter == 0)
                {
                    // shop method
                    LogHandler.Add("Shop Room" + " ShopCounter: " + Level.ShopCounter);
                    interactions.Shop();

                }
                else
                {
                    newLevel.InitRoom();
                    player.SetCoordinates(newLevel.entranceObject.x, newLevel.entranceObject.y);
                    EnemyGenerator enemyGenerator = new EnemyGenerator();
                    Enemy enemy1 = enemyGenerator.GenerateEnemy(Level.startingDifficulty);
                    interactions.ConnectEnemy(enemy1);
                    LogHandler.Add("Entered new room!");

                    //drawing and interactions inside level
                    while (!player.playerCP.IsDead())
                    {
                        UtilityMethods.ShouldClear();
                        Render();

                        if(!enemy1.enemyCombatParameters.IsDead())
                        {
                            //combat check
                            if (interactions.InRangeOfObject(newLevel.enemyObject))
                            {
                                //combat loop
                                player._inCombat = true;
                                interactions.Combat(enemy1);
                                if (Items.strBuffAmount > 0)
                                {
                                    Items.strBuffAmount--;
                                    if (Items.strBuffAmount == 0)
                                    {
                                        Items.strBuffDmg = 0;
                                    }
                                    else if (Items.strBuffAmount % 2 == 0)
                                    {
                                        Items.strBuffDmg -= 2;
                                    }
                                    LogHandler.Add($"{Items.strBuffAmount} Strength-buffed Combat remaining");
                                }
                                UtilityMethods.shouldClear = true;
                            }
                        }
                        if (interactions.OnObject())
                        {
                            interactions.InteractOnObject();
                            Render();
                        }

                        newLevel.UserActions();
                        if(!enemy1.enemyCombatParameters.IsDead())
                        {
                            newLevel.EnemyMovement(player);
                        }

                        Console.SetCursorPosition(0, 0);
                        if(enemy1.enemyCombatParameters.IsDead())
                        {
                            if (newLevel.OnExit())
                            {
                                Level.startingDifficulty+= Level.progressionDifficulty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void EveryNewRoom(Player player)
        {
            Level.ShopCounter--;
            if (Items.healthRegenAmount > 0 || Items.shieldRegenAmount > 0)
            {
                if (player.playerCP._currentHP + Items.healthRegenAmount >= player.playerCP._maxHP)
                {
                    player.playerCP._currentHP = player.playerCP._maxHP;
                }
                else
                {
                    player.playerCP._currentHP += Items.healthRegenAmount;
                }

                player.playerCP.shield += Items.shieldRegenAmount;
            }
        }

        private void Render()
        {
            newLevel.Draw();
            Console.SetCursorPosition(0, newLevel.GetRows() + 7);
            UtilityMethods.ClearBlock(60, 15);
            Console.SetCursorPosition(0, newLevel.GetRows() + 7);
            LogHandler.PrintShortLog();
            Console.SetCursorPosition(0, 0);
        }

        public void Start()
        {
            bool startGame = false;
            string choice;
            Console.WriteLine("WELCOME TO");
            UtilityMethods.Title();
            Console.WriteLine();
            Console.WriteLine("FULL SCREEN FOR BEST EXPERIENCE!");
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
            while(!startGame)
            {
                ExplantaionStation();
                Console.WriteLine("press 1 to change Player color or symbol");
                Console.WriteLine("press 2 to change Enemy color or symbol");
                Console.WriteLine($"press 3 to choose difficulty. {Menu.DifficultyToWords()}");
                Console.WriteLine("press 0 to continue");
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
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
                    case "0":
                        startGame = true;
                        break;
                    default:
                        break;
                }
            }
            Play();
        }

        

        

        public void ExplantaionStation()
        {
            Console.Clear();
            Console.WriteLine("EXPLANATION STATION");
            Console.WriteLine("Symbols and their meaning:");
            Console.WriteLine();

            Console.Write("Your Character: ");
            Console.ForegroundColor = Menu.playerColor;
            Console.WriteLine(Player.playerSymbol);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.Write("Current HP: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Player.pHealthSymbol);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Empty HP: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Player.pHealthSymbol);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Shield: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Player.pHealthSymbol);
            Console.ForegroundColor = ConsoleColor.White;


            Console.Write("HP Example: " );
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Player.pHealthSymbol}{Player.pHealthSymbol}{Player.pHealthSymbol}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{Player.pHealthSymbol}{Player.pHealthSymbol}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Player.pHealthSymbol}{Player.pHealthSymbol}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.Write("Currency: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Coin");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Entrance: " + Level.mapSymbols[Level.symbolEnum.entranceSymbol]);
            Console.WriteLine("Exit: " + Level.mapSymbols[Level.symbolEnum.exitSymbol]);

            Console.Write("Enemy: "); 
            Console.ForegroundColor = Menu.enemyColor;
            Console.WriteLine(Level.mapSymbols[Level.symbolEnum.enemySymbol]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Treasure: " + Level.mapSymbols[Level.symbolEnum.treasureSymbol]);
            Console.WriteLine("Spikes: " + Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
            Console.WriteLine("Walls: " + Level.mapSymbols[Level.symbolEnum.horizontal] + " & " + Level.mapSymbols[Level.symbolEnum.vertical]);
            Console.WriteLine();
            Console.WriteLine("Traps are invisible, but when they are triggered they release Spikes around the Player");
            Console.WriteLine("Movement with WASD");
            Console.WriteLine("Press Escape to open Menu");
            Console.WriteLine("Must kill the enemy to progress to the next room");
            Console.WriteLine();

        }
    }
}
