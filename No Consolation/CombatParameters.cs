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

        private static int _tmpTotalDamage = 1;

        Random _random = new Random();

        //init HP, Damage, Dodge Chance
        public CombatParameters(int maxHP, int damage, int dodgeChance)
        {
            if (maxHP <= 0)
                maxHP = 1;

            if (damage <= 0)
                damage = 1;

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
            return _random.Next(0, 10) >= _dodgeChance;
        }

        //public bool IsAttackHit(int num)
        //{
        //    return _random.Next(0, 10) >= _dodgeChance + num;
        //}

        public void TakeDamage(int damage)
        {
            if (damage >= _currentHP)
            {
                _currentHP = 0;
            }
            else _currentHP -= damage;
        }

        //public void TakeDamage(int damage, Shield shield)
        //{
        //    int damageAfterShield;
        //    damageAfterShield = (int)(damage * shield.GetShieldBlockPercent());
        //    Console.WriteLine(damage + " - damage before shield. " + damageAfterShield + " - damage after shield");
        //    TakeDamage(damageAfterShield);
        //}

        //public void TakeDamage(Weapon weaponStats, bool dodgeActive)
        //{
        //    _tmpTotalDamage = 0;

        //    //loop based on amout of attacks
        //    for (int i = 0; i < weaponStats.GetWeaponAttackAmount(); i++)
        //    {
        //        //calculate weapon damage (crit chance x damage)
        //        if (_random.Next(0, 11) <= weaponStats.GetWeaponCritChance())
        //        {
        //            //add to total damage (base damage + weapon damage loop)
        //            _tmpTotalDamage += (weaponStats.GetWeaponDamage() * 2);
        //        }
        //        else
        //        {
        //            //add to total damage (base damage + weapon damage loop)
        //            _tmpTotalDamage += weaponStats.GetWeaponDamage();
        //        }
        //    }
        //    if (dodgeActive)
        //    {
        //        _tmpTotalDamage /= 2;
        //    }
        //    //remove damage from currentHP
        //    TakeDamage(_tmpTotalDamage);
        //}

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
            for(int i = 1; i <= _maxHP; i++)
            {
                if(i <= _currentHP)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(_hpSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (i <= _startingHP)
                {
                    Console.Write(_hpSymbol);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(_hpSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        //public void PlayerAttack(Player player, Enemy enemy)
        //{
        //    if (enemy.enemyCombatParameters.HitOrMiss())
        //    {
        //        enemy.enemyCombatParameters.TakeDamage(player.playerWeapon, player.dodgeActive);
        //        Console.WriteLine($"you {player.playerWeapon.GetWeaponAttackType()} with your {player.playerWeapon.GetWeaponName()} dealing {player.playerCombatParameters.GetTmpTotalDamage()} damage!");
        //        enemy.EnemyStatsRemaining(enemy);
        //    }
        //    else
        //    {
        //        Console.WriteLine("The attack missed!");
        //    }
        //}

        //public void EnemyAttacks(Enemy Enemy1, Player player1, bool dodgeActive)
        //{
        //    Console.WriteLine($"\n{Enemy1.PrintName()} attacks");
        //    if (dodgeActive)
        //    {
        //        if (player1.playerCombatParameters.HitOrMiss(4))
        //        {
        //            Console.WriteLine($"You take {Enemy1.enemyCombatParameters.GetDamage()} Damage");
        //            player1.playerCombatParameters.TakeDamage(Enemy1.enemyCombatParameters.GetDamage(), player1.PlayerShield);
        //            Console.WriteLine($"you have {player1.playerCombatParameters.GetCurrentHP()} HP remaining");
        //        }
        //        else
        //        {
        //            Console.WriteLine("\nThe attack missed!");
        //        }
        //    }
        //    else
        //    {
        //        if (player1.playerCombatParameters.HitOrMiss())
        //        {
        //            Console.WriteLine($"You take {Enemy1.enemyCombatParameters.GetDamage()} Damage");
        //            player1.playerCombatParameters.TakeDamage(Enemy1.enemyCombatParameters.GetDamage(), player1.PlayerShield);
        //            Console.WriteLine($"you have {player1.playerCombatParameters.GetCurrentHP()} HP remaining");
        //        }
        //        else
        //        {
        //            Console.WriteLine("\nThe attack missed!");
        //        }
        //    }
        //}
    }
}