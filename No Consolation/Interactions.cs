using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    internal class Interactions
    {
        Player player;
        Level level;
        

        public void ConnectComponents(Player player, Level level)
        {
            this.level = level;
            this.player = player;
        }

        private void InteractOnObject(MapObject mObj)
        {
            char c = Level.mapSymbols[Level.symbolEnum.treasureSymbol];
            switch (mObj._symbol)
            {
                case '$':
                    //treasure interaction dont forget update log
                    break;
                case 'T':
                    //trap springs spikes interaction dont 4get log
                    break;
                case '◘':
                    //appear around player in 2 - 3 random directions that are available.
                    //spikes 1 damage to player then removes them... log


                    break;
                default:
                    break;
            }
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

        public void OnObject()
        {
            
            foreach(MapObject obj in level.mapObjects)
            {
                if(player.GetX() == obj.x && player.GetY() == obj.y)
                {
                    InteractOnObject(obj);
                }
            }

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
                    enemy.EnemyStatsRemaining(enemy);
                    Console.WriteLine();
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

        private void EnemyAttakcs(Enemy enemy)
        {
            Console.WriteLine($"{enemy.PrintName()} attacks!");
            player.playerCP.TakeDamage(enemy.enemyCombatParameters.GetDamage());
        }

        public void PlayerOptions(Enemy enemy)
        {
            Console.WriteLine($"Choose an option: \n1. Attack for {player.playerCP.GetDamage()} damage\n2. Run (50% chance of success)");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
            case 1:
                Console.WriteLine($"You attack for {player.playerCP.GetDamage()} damage");
                if (player.playerCP.IsAttackHit())
                {
                    Console.WriteLine("Attack hits!");
                    enemy.enemyCombatParameters.TakeDamage(player.playerCP.GetDamage());
                }
                else
                {
                    Console.WriteLine("Attack missed");
                }
                break;
            case 2:
                if (player.playerCP.Run())
                {
                    Console.WriteLine("You ran away!");
                    level.RemoveObjectSymbol(level.enemyObject);
                    player._inCombat = false;
                }
                else
                {
                    Console.WriteLine("You tripped and couldnt run away");
                }
                break;
            }
        }
    }
}
