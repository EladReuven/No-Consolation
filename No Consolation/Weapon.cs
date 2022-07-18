using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class Weapon
    {
        private static int[] weaponDamageList = new int[] { 8, 4, 7, 3};
        private int weaponDamage;

        private static string[] weaponNameList = new string[] { "Axe", "Spear", "Sword", "Nunchucks" };
        private string weaponName;

        private static string[] weaponAttackTypeList = new string[] { "chop", "jab", "slice", "swing"};
        private string weaponAttackType;

        private static int[] weaponAttackAmountList = new int[] {1, 2, 1, 3};
        private int weaponAttackAmount;

        private static int[] weaponCritChanceList = new int[] {3, 7, 5, 4};
        private int weaponCritChance; //out of 10 = 100%
        

        public Weapon(int weaponChoice)
        {
            this.weaponDamage = weaponDamageList[weaponChoice - 1];
            this.weaponName = weaponNameList[weaponChoice - 1];
            this.weaponAttackType = weaponAttackTypeList[weaponChoice - 1];
            this.weaponAttackAmount = weaponAttackAmountList[weaponChoice - 1];
            this.weaponCritChance = weaponCritChanceList[weaponChoice - 1];
        }

        public int GetWeaponDamage()
        {
            return this.weaponDamage;
        }
        public string GetWeaponName()
        {
            return this.weaponName;
        }
        public string GetWeaponAttackType()
        {
            return this.weaponAttackType;
        }
        public int GetWeaponAttackAmount()
        {
            return this.weaponAttackAmount;
        }
        public int GetWeaponCritChance()
        {
            return this.weaponCritChance;
        }

        public static void ChooseYourWeaponText()
        {
            Console.WriteLine($"\n1 {Weapon.PrintWeaponStats((int)weapons.Axe)} \n2 {Weapon.PrintWeaponStats((int)weapons.Spear)} \n3 {Weapon.PrintWeaponStats((int)weapons.Sword)} \n4 {Weapon.PrintWeaponStats((int)weapons.Nunchucks)}");
        }

        public static string PrintWeaponStats(int num)
        {
            return $"'{weaponNameList[num]}', {weaponDamageList[num]} Damage, {weaponAttackAmountList[num]} Hits, {weaponCritChanceList[num]}/10 Crit Chance";
        }
        public enum weapons
        {
            Axe,
            Spear,
            Sword,
            Nunchucks
        }

    }
}
