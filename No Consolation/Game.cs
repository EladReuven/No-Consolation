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
                newLevel.SetPlayer(player);
                interactions.ConnectComponents(player, newLevel);
                newLevel.InitRoom();
                player.SetCoordinates(newLevel.entranceObject.x, newLevel.entranceObject.y);
                EnemyGenerator enemyGenerator = new EnemyGenerator();
                Enemy enemy1 = enemyGenerator.GenerateEnemy(Level.difficulty);

                //drawing and interactions inside level
                while (!player.playerCP.IsDead())
                {
                    UtilityMethods.ShouldClear();
                    Render();

                    //combat check
                    if (interactions.InRangeOfObject(Level.mapSymbols[Level.symbolEnum.enemySymbol]))
                    {
                        //combat loop
                        player._inCombat = true;
                        interactions.Combat(enemy1);
                        LogHandler.Add($"Encountered {enemy1.PrintName()} enemy");
                        UtilityMethods.shouldClear = true;
                    }
                    
                    //else if(interactions.OnObject(Level.mapSymbols[(int)Level.symbolEnum.treasureSymbol]))
                    //{
                    //    treasure interacion

                    //    newLevel.RemoveObjectSymbol(newLevel.treasureObject);
                    //}

                    newLevel.PlayerMovement();

                    Console.SetCursorPosition(0,0);
                    if (newLevel.OnExit())
                    {
                        LogHandler.Add($"Entered new room!");

                        Level.difficulty++;
                        break;
                    }
                }
            }
        }

        private void Render()
        {
            newLevel.Draw();
            Console.SetCursorPosition(0, newLevel.GetRows() + 5);
            LogHandler.PrintLog();
            Console.SetCursorPosition(0, 0);
        }

    }
}
