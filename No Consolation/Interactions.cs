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
        Player player;
        Level level;

        public void ConnectComponents(Player player, Level level)
        {
            this.level = level;
            this.player = player;
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
                }
            }
            //char c = Level.mapSymbols[Level.symbolEnum.treasureSymbol];
        }

        public bool InRangeOfObject(char symbol)
        {
            for(int j = player.GetY() - 1; j <= player.GetY() + 1; j++)
            {
                for(int i = player.GetX() - 1; i <= player.GetX() + 1; i++)
                {
                    if (level.CharAtGridPos(i, j) == symbol)
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
            int choice;
            Console.SetCursorPosition(0, level.GetRows() + 1);
            while(player._inCombat)
            {
                // i dunno how im gonna do this but maybe combat here?
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
                        enemy.EnemyDeadText();
                        level.RemoveObjectSymbol(level.enemyObject);
                        player.coin += 5;
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
                        Console.WriteLine("You died");
                        player._inCombat = false;
                        break;
                    }
                }
            }
            player._inCombat = false;
            Console.SetCursorPosition(0, 0);
        }

        public void PlayerOptions(Enemy enemy)
        {
            Console.WriteLine($"Choose an option: \n1. Attack for {player.playerCP.GetDamage()} damage\n2. Run (50% chance of success)");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                //attack, check if hit, damage enemy
                case 1:
                    Console.WriteLine($"{player.GetName()} attacks for {player.playerCP.GetDamage()} damage");
                    if (enemy.enemyCombatParameters.IsAttackHit())
                    {
                        //Console.WriteLine("enemy dc: " + enemy.enemyCombatParameters._dodgeChance + " player dc:" + player.playerCP._dodgeChance);
                        Console.WriteLine("Attack hits!");
                        enemy.enemyCombatParameters.TakeDamage(player.playerCP.GetDamage());
                    }
                    else
                    {
                        //Console.WriteLine("enemy dc: " + enemy.enemyCombatParameters._dodgeChance + " player dc:" + player.playerCP._dodgeChance);
                        Console.WriteLine("Attack missed!");
                    }
                    break;
                //try run
                case 2:
                    if (player.playerCP.Run())
                    {
                        Console.WriteLine($"{player.GetName()} ran away!");
                        level.RemoveObjectSymbol(level.enemyObject);
                        player._inCombat = false;
                    }
                    else
                    {
                        Console.WriteLine($"{player.GetName()} tripped and could'nt run away!");
                    }
                    break;
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
            //debugging
            //Console.WriteLine($"trap x: {trap.x} trap y: {trap.y}");

            //level.SetGrid(trap.x, trap.y, trap);

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
                }
                else if (level.IsAvailable(trap.x, trap.y + 1))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x, trap.y + 1, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                }
                else if (level.IsAvailable(trap.x, trap.y - 1))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x, trap.y - 1, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
                }
                else if (level.IsAvailable(trap.x - 1, trap.y))
                {
                    level.spikeObject = new MapObject(level, "spike" + spikeCount + trap._name, trap.x - 1, trap.y, Level.mapSymbols[Level.symbolEnum.spikeSymbol]);
                    level.SetGrid(level.spikeObject.x, level.spikeObject.y, level.spikeObject);
                    level.mapObjects.Add(level.spikeObject);
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

    }
}
