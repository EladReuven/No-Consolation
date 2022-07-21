﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    internal class Items
    {
        //items to be the name of the item, and amout of items

        //str potion = str buff active (+2 dmg for next 2 enemies) NEED TEST

        //raise max hp item

        //raise max dmg

        //regenerating shield buff (+1 shield every new room)

        //regen hp buff active (+1 hp back every new room)

        public Player player;
        Random random = new Random();

        //strength buff combat counter, + every str potion, - every end of combat 
        public static int strBuff = 0;

        public Items()
        {
            shopItems = new List<itemEnum>
                {
                    itemEnum.bagOfCoins,
                    itemEnum.heal,
                    itemEnum.shield,
                    itemEnum.str,
                    itemEnum.maxHp,
                    itemEnum.maxDmg,
                    itemEnum.regenShield,
                    itemEnum.regenHP
                };
            treasureItems = new List<itemEnum>
                {
                    itemEnum.bagOfCoins,
                    itemEnum.heal,
                    itemEnum.shield,
                    itemEnum.str,
                    itemEnum.cursedCoins
                };
        }


        public enum itemEnum
        {
            bagOfCoins,
            heal,
            shield,
            str,
            maxHp,
            maxDmg,
            regenShield,
            regenHP,
            cursedCoins
        }

        public List<itemEnum> shopItems;
        public List<itemEnum> treasureItems;

        public static Dictionary<itemEnum, int> itemPrice = new Dictionary<itemEnum, int>()
        {
            {itemEnum.heal, 5 },
            {itemEnum.shield, 5 },
            {itemEnum.str, 5 },
            {itemEnum.maxHp, 8 },
            {itemEnum.maxDmg, 8 },
            {itemEnum.regenHP, 15 },
            {itemEnum.regenShield, 15 }
        };

        public static Dictionary<itemEnum, string> itemName = new Dictionary<itemEnum, string>()
        {
            {itemEnum.bagOfCoins, "Bag of Coins" },
            {itemEnum.heal, "Health Potion" },
            {itemEnum.shield, "Shield Potion" },
            {itemEnum.str, "Strength Potion" },
            {itemEnum.maxHp, "Max HP potion" },
            {itemEnum.maxDmg, "Max Damage" },
            {itemEnum.regenHP, "Shield Regen" },
            {itemEnum.regenShield, "Health Regen" },
            {itemEnum.cursedCoins, "Cursed coins" }
        };

        public static Dictionary<itemEnum, string> itemDescription = new Dictionary<itemEnum, string>()
        {
            {itemEnum.bagOfCoins, "Gain 2-4 coins" },
            {itemEnum.heal, "Heal 3 HP" },
            {itemEnum.shield, "Gain a 3 damage Shield" },
            {itemEnum.str, "Raise strength by 2 for the next 2 combats" },
            {itemEnum.maxHp, "Permenantly increase max HP by 2" },
            {itemEnum.maxDmg, "Permenantly increase your Damage by 1" },
            {itemEnum.regenHP, "Increase shield by 1 every new room for the rest of the game" },
            {itemEnum.regenShield, "Regenerate 1 HP every new room for the rest of the game" },
            {itemEnum.cursedCoins, "Gain 10 coins but the next room will have double the traps!" }
        };


        public void itemAction(itemEnum item)
        {
            switch(item)
            {
                case itemEnum.bagOfCoins:
                    BagOfCoins();
                    break;
                case itemEnum.heal:
                    Heal();
                    break;
                case itemEnum.str:
                    StrengthBuff();
                    break;
                case itemEnum.shield:
                    Shield();
                    break;
            }
        }

        private void Shield()
        {
            player.playerCP.shield += 3;
            LogHandler.Add("Gained 3 shield!");
        }

        private static void StrengthBuff()
        {
            strBuff += 2;
            LogHandler.Add("Drank a Strength Potion! Buffed for the next 2 combats!");
        }

        private void BagOfCoins()
        {
            int coinsGained = random.Next(2, 5);
            player.coin += coinsGained;
            LogHandler.Add($"Gained a bag of {coinsGained} Coins!");
        }

        private void Heal()
        {
            if (player.playerCP._currentHP + 3 < player.playerCP._maxHP)
            {
                player.playerCP._currentHP += 3;
                LogHandler.Add("Drank a healing potion! Restored 3 HP");
            }
            else if (player.playerCP._currentHP + 3 >= player.playerCP._maxHP)
            {
                player.playerCP._currentHP = player.playerCP._maxHP;
                LogHandler.Add($"Drank a healing potion! Restored {player.playerCP._maxHP - player.playerCP._currentHP} HP. You are now max HP!");
            }
        }
    }
}
