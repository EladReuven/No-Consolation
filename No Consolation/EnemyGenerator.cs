using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class EnemyGenerator
    {
        private string[] _nameList = new string[] { "Amit", "Other-Amit", "Avi", "Daniel", "Dor", "Eitan", "Saher", "Yarin", "Ziv"};
        private string[] _desc = new string[] {"Mighty", "Awful", "Average", "Foul", "Colossal", "Droopy", "Handsome", "Disgusting", "Ghostly"};

        private int _enemiesMade = 0;



        public Enemy GenerateEnemy(int difficulty)
        {
            _enemiesMade++;
            Random rand = new Random();
            int dif = (int)(rand.Next(1, difficulty));
            Enemy enemy = new Enemy(new CombatParameters(dif, difficulty - dif,1));
            enemy.NameAndDesc(_nameList[rand.Next(0, _nameList.Length)], _desc[rand.Next(0, _desc.Length)]);
            //MapObject enemyObject = new MapObject(enemy.PrintName(), 1, 1, enemy._enemySymbol);
            return enemy;
        }

        public int GetEnemiesMade()
        {
            return _enemiesMade;
        }

    }
}
