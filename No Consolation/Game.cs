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
            Console.WriteLine("enter your name: ");
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
                    Enemy enemy1 = enemyGenerator.GenerateEnemy(Level.difficulty);
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

                        newLevel.PlayerMovement();
                        if(!enemy1.enemyCombatParameters.IsDead())
                        {
                            newLevel.EnemyMovement(player);
                        }

                        Console.SetCursorPosition(0, 0);
                        if(enemy1.enemyCombatParameters.IsDead())
                        {
                            if (newLevel.OnExit())
                            {
                                Level.difficulty++;
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

    }
}
