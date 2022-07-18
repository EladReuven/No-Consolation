using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class Enemy
    {
        public char _enemySymbol = 'E';

        private string _name, _description;
        private int _enemyX, _enemyY;


        public CombatParameters enemyCombatParameters;

        public Enemy(CombatParameters parameters)
        {
            enemyCombatParameters = parameters;
        }

        public void SetPosition(int x, int y)
        {
            _enemyX = x;
            _enemyY = y;
        }


        public void NameAndDesc(string name, string desc)
        {
            this._name = name;
            this._description = desc;
        }

        public string PrintName()
        {
            return _description + " " + _name;
        }

        public void EnemyStatsRemaining(Enemy enemy)
        {
            Console.WriteLine($"{enemy.PrintName()} has {enemy.enemyCombatParameters.GetCurrentHP()} HP and {enemy.enemyCombatParameters.GetDamage()} Damage.");
        }

        public void EnemyEncounter()
        {
            Console.WriteLine($"\nyou encounter a {this.PrintName()} with {this.enemyCombatParameters.GetCurrentHP()} HP and {this.enemyCombatParameters.GetDamage()} Attack!");
            if (this.PrintName() == "Humble Dor")
            {
                Console.WriteLine($"\"Ten points to GryffinDor!\"");
            }
        }

        public void EnemyDeadText()
        {
            Console.WriteLine($"{_description} {_name} died!");
        }
    }
}
