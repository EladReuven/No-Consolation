using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class CombatParameters
    {
        public const char _hpSymbol = '♥';
        public int _startingHP, _maxHP, _currentHP, _damage, _dodgeChance;
        public int shield;

        private static int _tmpTotalDamage = 1;

        Random _random = new Random();

        //init HP, Damage, Dodge Chance
        public CombatParameters(int maxHP, int damage, int dodgeChance)
        {
            if (maxHP <= 0)
                maxHP = 1;

            if (damage <= 0)
                damage = 1;

            this.shield = 0;
            this._dodgeChance = dodgeChance; // out of 10
            this._maxHP = maxHP;
            this._startingHP = _maxHP;
            this._currentHP = this._maxHP;
            this._damage = damage;
        }


        public bool Run()
        {
            return _random.Next(0,2) == 1;
        }
        
        public bool IsAttackHit()
        {
            return _random.Next(0,10) >= _dodgeChance;
        }

        public void TakeDamage(int damage)
        {
            if(shield >= damage)
            {
                shield -= damage;
            }
            else if (shield < damage)
            {
                damage -= shield;
                shield = 0;

                if (damage >= _currentHP)
                {
                    _currentHP = 0;
                }
                else _currentHP -= damage;
            }
        }

        public bool IsDead()
        {
            return _currentHP <= 0;
        }

        public int GetCurrentHP()
        {
            return _currentHP;
        }

        public void AddCurrentHP(int num)
        {
            this._currentHP += num;
        }
        public void CurrentToMaxHP()
        {
            this._currentHP = _maxHP;
        }
        public int GetMaxHP()
        {
            return _maxHP;
        }

        //damage without weapon
        public int GetDamage()
        {
            return _damage;
        }

        //last damage dealt with weapon
        public int GetTmpTotalDamage()
        {
            return _tmpTotalDamage;
        }

        public void PrintHearts()
        {
            int maxHealthPlusShield = _maxHP;
            if (_currentHP + shield > _maxHP)
            {
                maxHealthPlusShield = _maxHP + shield;
            }

            for(int i = 1; i <= maxHealthPlusShield; i++)
            {
                if(i <= _currentHP)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(_hpSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (i <= _maxHP)
                {
                    Console.Write(_hpSymbol);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(_hpSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

    }
}