using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    internal class Interactions
    {
        Random rand = new Random();
        Items items = new Items();
        Player player;
        Level level;
        Enemy enemy;

        public bool ranAway = false;
        string response = "CHOOSE AN ITEM: ";

        public void ConnectComponents(Player player, Level level)
        {
            this.level = level;
            this.player = player;
            items.player = player;
        }

        public void ConnectEnemy(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void InteractOnObject()
        {
            MapObject[] objectsArr = new MapObject[level.mapObjects.Count];
            level.mapObjects.CopyTo(objectsArr, 0);

            foreach (MapObject obj in objectsArr)
            {
                if (player.GetX() == obj.x && player.GetY() == obj.y)
                {
                    switch (obj._symbol)
                    {
                        case '$':
                            //treasure interaction dont forget update log
                            StepOnTreasure(obj);
                            break;
                        case 'T':
                            //spawn 1-3 spikes around player, delete trap obj
                            ActivateTrap(obj);
                            break;
                        case '*':
                            //take 1 damage, delete spike object
                            StepOnSpike(obj);
                            break;
                        default:
                            break;
                    }
                    if(obj._symbol == Level.mapSymbols[Level.symbolEnum.enemySymbol])
                    {
                        Combat(enemy);
                    }
                }
            }
        }

        private void StepOnTreasure(MapObject treasure)
        {
            int randItem = rand.Next(items.treasureItems.Count);
            LogHandler.Add($"Opned a treasure chest and found a {Items.itemName[items.treasureItems[randItem]]}");
            items.itemAction(items.treasureItems[randItem]);
            level.RemoveMapObject(treasure);
            BeepMusic.CoinPickUp();
        }

        public bool InRangeOfObject(MapObject obj)
        {
            for(int j = player.GetY() - 1; j <= player.GetY() + 1; j++)
            {
                for(int i = player.GetX() - 1; i <= player.GetX() + 1; i++)
                {
                    if (obj.x == i && obj.y == j)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool OnObject()
        {
            foreach (MapObject obj in level.mapObjects)
            {
                if (player.GetX() == obj.x && player.GetY() == obj.y)
                {
                    return true;
                }
            }
            return false;
        }

        public void Combat(Enemy enemy)
        {
            ranAway = false;
            int choice;
            Console.SetCursorPosition(0, level.GetRows() + 1);
            while(player._inCombat)
            {
                enemy.EnemyEncounter();
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
                while(player._inCombat)
                {
                    Console.WriteLine("---------------------------");
                    enemy.EnemyStatsRemaining(enemy);
                    player.PlayerStatsRemaining();
                    Console.WriteLine("---------------------------");
                    PlayerOptions(enemy);
                    if (enemy.enemyCombatParameters.IsDead())
                    {
                        level.RemoveObjectSymbol(level.enemyObject);
                        level.RemoveMapObject(level.enemyObject);
                        if(!ranAway)
                        {
                            enemy.EnemyDeadText();
                            player.coin += 5;
                            LogHandler.Add($"You defeated the {enemy.PrintName()}! Gain 5 Coins");
                        }
                        player._inCombat = false;
                        break;
                    }
                    if(!player._inCombat)
                    {
                        break;
                    }
                    EnemyAttakcs(enemy);
                    if (player.playerCP.IsDead())
                    {
                        Console.WriteLine("You Died"); ;
                        player._inCombat = false;
                        Console.ReadLine();
                        break;
                    }
                }
            }
            player._inCombat = false;
            Console.SetCursorPosition(0, 0);
        }

        public void PlayerOptions(Enemy enemy)
        {
            bool loopBool = true;
            Console.WriteLine($"Choose an option: \n1. Attack for {player.playerCP.GetDamage() + Items.strBuffDmg} damage\n2. Run (50% chance of success)");
            while (loopBool)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    //attack, check if hit, damage enemy
                    case "1":
                        if (enemy.enemyCombatParameters.IsAttackHit())
                        {
                            if(Items.strBuffAmount > 0)
                            {
                                Console.WriteLine($"{player.GetName()} attacks for {player.playerCP.GetDamage() + Items.strBuffDmg} damage");
                                enemy.enemyCombatParameters.TakeDamage(player.playerCP.GetDamage() + Items.strBuffDmg);
                                Console.WriteLine("Attack hits!");
                                loopBool = false;
                            }
                            else
                            {
                                Console.WriteLine($"{player.GetName()} attacks for {player.playerCP.GetDamage()} damage");
                                enemy.enemyCombatParameters.TakeDamage(player.playerCP.GetDamage());
                                Console.WriteLine("Attack hits!");
                                loopBool = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Attack missed!");
                                loopBool = false;
                        }
                        break;
                    //try run
                    case "2":
                        if (player.playerCP.Run())
                        {
                            Console.WriteLine($"{player.GetName()} ran away!");
                            //level.RemoveObjectSymbol(level.enemyObject);
                            level.RemoveMapObject(level.enemyObject);
                            LogHandler.Add($"You ran away from the {enemy.PrintName()}. disgracful.");
                            player._inCombat = false;
                            loopBool = false;
                            enemy.enemyCombatParameters._currentHP = 0;
                            ranAway = true;
                        }
                        else
                        {
                            Console.WriteLine($"{player.GetName()} tripped and couldn't run away!");
                            loopBool = false;
                        }
                        break;
                    default:
                        Console.WriteLine("Enter a valid choice");

                        break;
                }
            }
        }

        private void EnemyAttakcs(Enemy enemy)
        {
            Console.WriteLine($"{enemy.PrintName()} attacks!");
            if(player.playerCP.IsAttackHit())
            {
                Console.WriteLine("Enemy attack hit!");
                player.playerCP.TakeDamage(enemy.enemyCombatParameters.GetDamage());
            }
            else
            {
                Console.WriteLine("Enemy attack missed!");
            }
        }

        private void ActivateTrap(MapObject trap)
        {

            //pick between 2-3 traps
            int surroundingTraps = rand.Next(1, 4);
            int spikeCount = 1;

            //place spike around player on x+-1 or y+-1 IF AVAILABLE
            for (int i = 0; i < surroundingTraps; i++)
            {
                //checks if position is available, make a spike obj and add to mapObjects list
                if (level.IsAvailable(trap.x + 1, trap.y))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x + 1, trap.y, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                    continue;
                }
                else if (level.IsAvailable(trap.x, trap.y + 1))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x, trap.y + 1, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                    continue;
                }
                else if (level.IsAvailable(trap.x, trap.y - 1))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x, trap.y - 1, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                    continue;
                }
                else if (level.IsAvailable(trap.x - 1, trap.y))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x - 1, trap.y, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                    continue;
                }
            }

            //remove current trap from list so wont trigger in the future
            level.RemoveMapObject(trap);

            //update Log
            LogHandler.Add("Activated Trap!");
        }

        private void StepOnSpike(MapObject spike)
        {
            player.playerCP.TakeDamage(1);
            level.RemoveMapObject(spike);
            LogHandler.Add("Stepped on a spike and took 1 damage! Ouch!");
        }

        public void Shop()
        {
            bool firstTime = true;
            bool inShop = true;
            string userinput;
            Level.ShopCounter += rand.Next(2, 5);
            while (inShop)
            {
                Console.Clear();
                player.DrawPlayerStats();
                Console.WriteLine("HELLO I AM TEMMIE THE SHOPKEEPER WHAT WOOF YOU LIKE TO BUY?");
                if (UtilityMethods.sadDog)
                {
                    UtilityMethods.SadShopDog();
                    UtilityMethods.sadDog = false;
                }
                else
                {
                    UtilityMethods.HappyShopDog();
                }
                Console.WriteLine(response);
                Console.WriteLine("-------------------------------------");
                PrintShop();
                if(firstTime)
                {
                    BeepMusic.DogSong();
                    firstTime = false;
                }
                //reads user choice
                userinput = Console.ReadLine();

                //switch with user choice
                switch (userinput)
                {
                    case "1":
                        ShopChoice(userinput);
                        break;
                    case "2":
                        ShopChoice(userinput);
                        break;
                    case "3":
                        ShopChoice(userinput);

                        break;
                    case "4":
                        ShopChoice(userinput);

                        break;
                    case "5":
                        ShopChoice(userinput);

                        break;
                    case "6":
                        ShopChoice(userinput);

                        break;
                    case "7":
                        ShopChoice(userinput);

                        break;
                    case "8":
                        //sad dog
                        UtilityMethods.sadDog = true;
                        break;
                    case "0":
                        inShop = false;
                        break;
                    default:
                        break;
                }

                //check if player has enough coins for item
                //if yes, buy and activate item
                //if no write not enough money
                //if chose nothing then sad dog
            }

        }

        private void PrintShop()
        {
            int count = 0;
            foreach (Items.itemEnum i in items.shopItems)
            {
                count++;
                if (count == 6)
                {
                    Console.Write("6. Health Regen! ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"PRICE: {Items.itemPrice[Items.itemEnum.regenHP]}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"DESC: {Items.itemDescription[Items.itemEnum.regenHP]}");
                    Console.WriteLine();
                }
                else if (count == 7)
                {
                    Console.Write("7. Shield Regen! ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"PRICE: {Items.itemPrice[Items.itemEnum.regenShield]}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"DESC: {Items.itemDescription[Items.itemEnum.regenShield]}");
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"{count}. {Items.itemName[items.shopItems[count - 1]]}! ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"PRICE: {Items.itemPrice[items.shopItems[count - 1]]}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"DESC: {Items.itemDescription[items.shopItems[count - 1]]}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("8. NOTHING");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
        }

        private void ShopChoice(string userinput)
        {
            if (player.coin < Items.itemPrice[items.shopItems[int.Parse(userinput) - 1]])
            {
                response = $"YOU DONT HAVE ENOUGH COIN FOR A {Items.itemName[items.shopItems[int.Parse(userinput) - 1]].ToUpper()}";
                //Console.WriteLine($"You dont have enough Coin for a {Items.itemName[items.shopItems[int.Parse(userinput) - 1]]}");
            }
            else
            {
                player.coin -= Items.itemPrice[items.shopItems[int.Parse(userinput) - 1]];
                items.itemAction(items.shopItems[int.Parse(userinput) - 1]);
                response = "THANK YOU";
            }
        }
    }
}
