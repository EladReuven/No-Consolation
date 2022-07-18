using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{


    public class MapObject
    {

        // i know its public pls dont kill me
        public int[] _objectPos = new int[2];
        public int x, y, topLeftX, topLeftY, bottomRightX, bottomRightY;
        public string _name;
        public char _symbol;
        public bool active;


        public MapObject(Level level, string name, int x, int y, char symbol)
        {
            this.x = x;
            this.y = y;
            _name = name;
            active = true;
            _objectPos[0] = x;
            _objectPos[1] = y;
            _symbol = symbol;
        }

        public MapObject(Level level, string name, int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
        {
            this.topLeftX = topLeftX;
            this.topLeftY = topLeftY;
            this.bottomRightX = bottomRightX;
            this.bottomRightY = bottomRightY;

            _name = name;
        }
    }
}
