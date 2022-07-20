using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class Player
    {

        public CombatParameters playerCP;

        //grid stats
        public const char playerSymbol = '☻';
        public const char pHealthSymbol = '♥';

        private int _playerX;
        private int _playerY;
        private int[] _playerGridPos = new int[2];

        //player stats
        private string _name;
        public bool _inCombat;
        public int coin = 0;




        public Player(string name, int hp, int damage, int dodgeChance)
        {
            this.playerCP = new CombatParameters(hp, damage, dodgeChance);
            this._name = name;
            this._inCombat = false;
        }

        public void SetCoordinates(int x,int y)
        {
            _playerX = x;
            _playerY = y;
            _playerGridPos[0] = x;
            _playerGridPos[1] = y;
        }

        public int GetX()
        {
            return _playerX;
        }
        public int GetY()
        {
            return _playerY;
        }

        public char GetSymbol()
        {
            return playerSymbol;
        }
        public string GetName()
        {
            return _name;
        }

        public void DrawPlayerStats(Player player)
        {
            Console.Write("HP: ");
            player.playerCP.PrintHearts();
            Console.SetCursorPosition(30, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Coins: " + player.coin);
            Console.ForegroundColor = ConsoleColor.White;
            //Console.SetCursorPosition(0,0);
            for (int i = 0; i <= 40; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        public void PlayerStatsRemaining()
        {
            Console.WriteLine($"{_name} has {playerCP.GetCurrentHP()} HP remaining.");
        }

    }
}
