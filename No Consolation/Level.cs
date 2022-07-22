using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No_Consolation
{
    public class Level
    {

        Random rnd = new Random();

        public MapObject exitObject;
        public MapObject entranceObject;
        public MapObject treasureObject;
        public MapObject playerObject;
        public MapObject enemyObject;
        public MapObject blockie1, blockie2;
        public MapObject baseOfTrapObject;
        public MapObject spikeObject;
        public List<MapObject> mapObjects;


        Player player;
        Stack<Enemy> enemyStack = new Stack<Enemy>();
        EnemyGenerator enemyGenerator = new EnemyGenerator();

        private char[,] _grid;
        private int _colsX, _rowsY;

        public static int ShopCounter = 4;
        public static int difficulty = 5;


        public enum symbolEnum
        {
            openSpace,
            blockedSpace,
            horizontal,
            vertical,
            entranceSymbol,
            exitSymbol,
            treasureSymbol,
            enemySymbol,
            trapSymbol,
            spikeSymbol,
            shopSymbol
        }

        public static Dictionary<symbolEnum, char> mapSymbols = new Dictionary<symbolEnum, char>()
        {
            {symbolEnum.openSpace, ' '},
            {symbolEnum.blockedSpace, '#'},
            {symbolEnum.horizontal, '═'},
            {symbolEnum.vertical, '║'},
            {symbolEnum.entranceSymbol, 'O'},
            {symbolEnum.exitSymbol, 'X'},
            {symbolEnum.treasureSymbol, '$'},
            {symbolEnum.enemySymbol, 'M'},
            {symbolEnum.trapSymbol, 'T'},
            {symbolEnum.spikeSymbol, '*'},
            {symbolEnum.shopSymbol, '♫' }
        };

        //private char _openSpace = ' ';
        //private char _blockedSpace = '#'; //█
        //private char _horizontal = '═';
        //private char _vertical = '║';
        //private char _entranceSymbol = 'E';
        //private char _exitSymbol = 'X';
        //private char _treasureSymbol = '$';
        //private char _enemySymbol = 'O';

        private char[] _notWalkable = new char[7] { '#', '═', '║', '╗', '╝', '╔', '╚' };

        private bool _onExit = false;
        public int numOfTraps;




        public Level()
        {
            char[,] grid = new char[rnd.Next(15, 21), rnd.Next(15, 21)];
            _grid = grid;
            _colsX = _grid.GetLength(0);
            _rowsY = _grid.GetLength(1);
            mapObjects = new List<MapObject>();
            
        }

        public void InitRoom()
        {
            LevelOutline();
            blockie1 = LevelBlock();
            blockie2 = LevelBlock();
            EntrancePos();
            ExitPos();
            TreasurePos();
            EnemyPos();
            TrapPos();
        }

        //check if space in room is available to have something new placed there
        public bool IsAvailable(int x, int y)
        {
            if (x < 1 || y < 1 || x >= _colsX || y >= _rowsY)
            {
                return false;
            }
            if (_grid[x, y] == mapSymbols[(symbolEnum.openSpace)])
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //switch for moving player with arrows
        public void PlayerMovement()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (IsWalkable(player.GetX(), player.GetY() - 1))
                    {
                        _grid[player.GetX(), player.GetY()] = mapSymbols[symbolEnum.openSpace];
                        player.SetCoordinates(player.GetX(), player.GetY() - 1);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (IsWalkable(player.GetX(), player.GetY() + 1))
                    {
                        _grid[player.GetX(), player.GetY()] = mapSymbols[symbolEnum.openSpace];
                        player.SetCoordinates(player.GetX(), player.GetY() + 1);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (IsWalkable(player.GetX() + 1, player.GetY()))
                    {
                        _grid[player.GetX(), player.GetY()] = mapSymbols[symbolEnum.openSpace];
                        player.SetCoordinates(player.GetX() + 1, player.GetY());
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (IsWalkable(player.GetX() - 1, player.GetY()))
                    {
                        _grid[player.GetX(), player.GetY()] = mapSymbols[symbolEnum.openSpace];
                        player.SetCoordinates(player.GetX() - 1, player.GetY());
                    }
                    break;
            }
        }

        //Enemy Movement
        public void EnemyMovement(Player player)
        {
            int tmpX = enemyObject.x;
            int tmpY = enemyObject.y;
            //This is gross code but the only other way i could think of was using recursion to try and find the fewest moves to player,
            //but i dunno how to do that... in the amount of time i have
            if(EnemyFoundPlayer(enemyObject))
            {
                //checking diagonal top left
                if(enemyObject.x > player.GetX() && enemyObject.y > player.GetY())
                {
                    if (IsAvailable(enemyObject.x - 1, enemyObject.y - 1))
                    {
                        enemyObject.x -= 1;
                        enemyObject.y -= 1;
                    }
                }
                //checking diagonal bottom left
                else if (enemyObject.x > player.GetX() && enemyObject.y < player.GetY())
                {
                    if (IsAvailable(enemyObject.x - 1, enemyObject.y + 1))
                    {
                        enemyObject.x -= 1;
                        enemyObject.y += 1;
                    }
                }
                //checking diagonal top right
                else if (enemyObject.x < player.GetX() && enemyObject.y < player.GetY())
                {
                    if (IsAvailable(enemyObject.x + 1, enemyObject.y + 1))
                    {
                        enemyObject.x += 1;
                        enemyObject.y += 1;
                    }
                }
                //checking diagonal bottom right
                else if (enemyObject.x < player.GetX() && enemyObject.y > player.GetY())
                {
                    if (IsAvailable(enemyObject.x + 1, enemyObject.y - 1))
                    {
                        enemyObject.x += 1;
                        enemyObject.y -= 1;
                    }
                }
                //checking only X movement
                else if (enemyObject.x > player.GetX())
                {
                    if (IsAvailable(enemyObject.x - 1, enemyObject.y))
                        enemyObject.x -= 1;
                }
                else if(enemyObject.x < player.GetX())
                {
                    if (IsAvailable(enemyObject.x + 1, enemyObject.y))
                        enemyObject.x += 1;
                }
                //checking only Y movement
                else if(enemyObject.y > player.GetY())
                {
                    if(IsAvailable(enemyObject.x, enemyObject.y - 1))
                        enemyObject.y -= 1;
                }
                else if(enemyObject.y < player.GetY())
                {
                    if (IsAvailable(enemyObject.x, enemyObject.y + 1))
                        enemyObject.y += 1;
                }
                _grid[tmpX, tmpY] = mapSymbols[symbolEnum.openSpace];
                _grid[enemyObject.x, enemyObject.y] = enemyObject._symbol;
            }
        }

        //check if player is in a square around the enemy
        public bool EnemyFoundPlayer(MapObject enemy)
        {
            for(int i = 0; i < 7;i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    if(enemy.x - 3 + j == player.GetX() && enemy.y - 3 + i == player.GetY())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //walkable for player
        public bool IsWalkable(int x, int y)
        {
            //level boundaries
            if (x < 1 || y < 1 || x >= _colsX - 1 || y >= _rowsY - 1)
            {
                return false;
            }

            //check if in not walkable array
            foreach (char symbol in _notWalkable)
            {
                if (_grid[x, y] == symbol)
                {
                    return false;
                }
            }
            return true;

        }


        //sets random entrance position
        public void EntrancePos()
        {
            int EntranceY;
            while (true)
            {
                EntranceY = rnd.Next(1, _rowsY - 1);
                if (IsAvailable(1, EntranceY))
                {
                    entranceObject = new MapObject(this,"Entrance", 1, EntranceY, mapSymbols[symbolEnum.entranceSymbol]);
                    mapObjects.Add(entranceObject);
                    _grid[entranceObject.x, entranceObject.y] = entranceObject._symbol;

                    break;
                }
            }
        }


        //sets random exit position
        public void ExitPos()
        {
            int ExitY;
            while (true)
            {
                ExitY = rnd.Next(1, _rowsY - 1);
                if (IsAvailable(_colsX - 2, ExitY))
                {
                    exitObject = new MapObject(this, "Exit", _colsX - 2, ExitY, mapSymbols[symbolEnum.exitSymbol]);
                    mapObjects.Add(exitObject);
                    _grid[exitObject.x, exitObject.y] = exitObject._symbol;
                    break;
                }
            }
        }


        //if player is on exit position
        public bool OnExit()
        {
            if (player.GetX() == exitObject.x && player.GetY() == exitObject.y)
            {
                _onExit = true;
            }
            else _onExit = false;
            return _onExit;
        }


        //sets random treasure position
        public void TreasurePos()
        {
            int randomX, randomY;
            while (true)
            {
                randomX = rnd.Next(1, _colsX - 2);
                randomY = rnd.Next(1, _rowsY - 1);
                if (IsAvailable(randomX, randomY))
                {
                    treasureObject = new MapObject(this, "Treasure", randomX, randomY, mapSymbols[(symbolEnum.treasureSymbol)]);
                    mapObjects.Add(treasureObject);
                    _grid[treasureObject.x, treasureObject.y] = treasureObject._symbol;
                    break;
                }
            }
        }


        //generates an enemy
        public void EnemyPos()
        {
            //enemyStack.Push(enemyGenerator.GenerateEnemy(difficulty));
            int randomX, randomY;
            while (true)
            {
                randomX = rnd.Next(1, _colsX - 2);
                randomY = rnd.Next(1, _rowsY - 1);
                if (IsAvailable(randomX, randomY))
                {
                    enemyObject = new MapObject(this, "Enemy", randomX, randomY, mapSymbols[(symbolEnum.enemySymbol)]);
                    mapObjects.Add(enemyObject);
                    _grid[enemyObject.x, enemyObject.y] = enemyObject._symbol;
                    break;
                }
            }
        }

        public void TrapPos()
        {
            int randomX, randomY;
            int eSpace = 0;
            int trapNumName = 1;
            
            foreach(char c in _grid)
            {
                if(c == mapSymbols[symbolEnum.openSpace])
                {
                    eSpace++;
                }
            }
            if(Items.cursedCoinActive)
            {
                numOfTraps = (int)(eSpace * 0.125f);
                Items.cursedCoinActive = false;
            }
            else
            {
                numOfTraps = (int)(eSpace * 0.05f);
            }

            for (int i = 0; i < numOfTraps; i++)
            {
                while(true)
                {
                    randomX = rnd.Next(1, _colsX - 2);
                    randomY = rnd.Next(1, _rowsY - 1);
                    if (IsAvailable(randomX, randomY))
                    {
                        baseOfTrapObject = new MapObject(this, "Trap Base " + trapNumName, randomX, randomY, mapSymbols[symbolEnum.trapSymbol]);
                        mapObjects.Add(baseOfTrapObject);
                        //_grid[baseOfTrapObject.x, baseOfTrapObject.y] = baseOfTrapObject._symbol;
                        trapNumName++;
                        break;
                    }
                }
            }
        }


        //set random enemy position
        //public void EnemyPosition()
        //{
        //    int randomX, randomY;
        //    while (true)
        //    {
        //        randomX = rnd.Next(1, _rows - 2);
        //        randomY = rnd.Next(1, _cols - 1);
        //        if (IsAvailable(randomX, randomY))
        //        {
        //            enemyObject = new MapObject("Enemy", randomX, randomY, _enemySymbol);
        //            _grid[enemyObject.x, enemyObject.y] = enemyObject._symbol;
        //            break;
        //        }
        //    }
        //}


        //connects room player with inserted player
        public void SetPlayer(Player player)
        {
            this.player = player;
        }


        //sets player position
        public void PlayerPos()
        {
            _grid[player.GetX(), player.GetY()] = player.GetSymbol();
        }


        //set level outline
        private void LevelOutline()
        {

            for (int y = 0; y < _rowsY; y++)
            {
                for (int x = 0; x < _colsX; x++)
                {
                    _grid[x, y] = mapSymbols[(symbolEnum.openSpace)];

                    if (x == _colsX - 1 && y == 0)
                    {
                        _grid[x, y] = '╗';
                    }
                    else if (x == _colsX - 1 && y == _rowsY - 1)
                    {
                        _grid[x, y] = '╝';
                    }
                    else if (x == 0 && y == 0)
                    {
                        _grid[x, y] = '╔';
                    }
                    else if (x == 0 && y == _rowsY - 1)
                    {
                        _grid[x, y] = '╚';
                    }
                    else if (x == 0 || x == _colsX - 1)
                    {
                        _grid[x, y] = mapSymbols[(symbolEnum.vertical)];
                    }
                    else if (y == 0 || y == _rowsY - 1)
                    {
                        _grid[x, y] = mapSymbols[(symbolEnum.horizontal)];
                    }
                }
            }
        }


        //set and draw a block on map
        private MapObject LevelBlock()
        {
            int topLeftX = rnd.Next(2, _colsX - 8);
            int topLeftY = rnd.Next(2, _rowsY - 8);
            int bottomRightX = rnd.Next(topLeftX + 2, _colsX - 2);
            int bottomRightY = rnd.Next(topLeftY + 2, _rowsY - 2);
            MapObject Block = new MapObject(this, "Block", topLeftX, topLeftY, bottomRightX, bottomRightY);
            mapObjects.Add(Block);

            for (int y = 0; y < _rowsY; y++)
            {
                for (int x = 0; x < _colsX; x++)
                {
                    if ((x <= bottomRightX && x >= topLeftX) && (y <= bottomRightY && y >= topLeftY))
                    {
                        _grid[x, y] = mapSymbols[(symbolEnum.blockedSpace)];

                        if (x == bottomRightX && y == topLeftY)
                        {
                            _grid[x, y] = '╗';
                        }
                        else if (x == bottomRightX && y == bottomRightY)
                        {
                            _grid[x, y] = '╝';
                        }
                        else if (x == topLeftX && y == topLeftY)
                        {
                            _grid[x, y] = '╔';
                        }
                        else if (x == topLeftX && y == bottomRightY)
                        {
                            _grid[x, y] = '╚';
                        }
                        else if (y == topLeftY || y == bottomRightY)
                        {
                            _grid[x, y] = mapSymbols[(symbolEnum.horizontal)];
                        }
                        else if (x == topLeftX || x == bottomRightX)
                        {
                            _grid[x, y] = mapSymbols[(symbolEnum.vertical)];
                        }
                    }
                }

            }
            return Block;
        }


        //draw everything in map
        public void Draw()
        {
            //insert objects symbols to grid
            _grid[entranceObject.x, entranceObject.y] = entranceObject._symbol;
            _grid[exitObject.x, exitObject.y] = exitObject._symbol;
            //_grid[treasureObject.x, treasureObject.y] = treasureObject._symbol;
            PlayerPos();

            //draw stats
            player.DrawPlayerStats();

            //draw map
            for (int y = 0; y < _rowsY; y++)
            {
                for (int x = 0; x < _colsX; x++)
                {
                    //map boundaries
                    if (y == 0 && x > 0 && x < _colsX - 1 || y == _rowsY - 1 && x > 0 && x < _colsX - 1)
                        Console.Write($"{mapSymbols[(symbolEnum.horizontal)]}{mapSymbols[(symbolEnum.horizontal)]}");
                    else if (y == 0 && x == 0 || y == _rowsY - 1 && x == 0)
                    {
                        Console.Write($"{_grid[x, y]}{mapSymbols[(symbolEnum.horizontal)]}");
                    }
                    else if ((x < blockie1.bottomRightX && x >= blockie1.topLeftX && (y == blockie1.bottomRightY || y == blockie1.topLeftY))
                        || (x < blockie2.bottomRightX && x >= blockie2.topLeftX && (y == blockie2.bottomRightY || y == blockie2.topLeftY)))
                    {
                        Console.Write($"{_grid[x, y]}{mapSymbols[(symbolEnum.horizontal)]}");
                    }
                    else if (x == player.GetX() && y == player.GetY())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(_grid[x, y] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //objects
                    else
                    {
                        Console.Write(_grid[x, y] + " ");
                    }
                }
                Console.WriteLine();
            }
            //Console.SetCursorPosition(player.GetX(),player.GetY());
        }

        public int GetRows()
        {
            return _rowsY;
        }

        public int GetCols()
        {
            return _colsX;
        }

        public void SetGrid(int x, int y, MapObject obj)
        {
            this._grid[x, y] = obj._symbol;
        }

        public char CharAtGridPos(int x, int y)
        {
            return _grid[x, y];
        }

        public void RemoveObjectSymbol(MapObject mapObject)
        {
            _grid[mapObject.x, mapObject.y] = Level.mapSymbols[(symbolEnum.openSpace)];
        }

        //removes object from mapObjects list
        public void RemoveMapObject(MapObject obj)
        {
            mapObjects.Remove(obj);
        }

    }
}
