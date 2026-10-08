using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class Wall : LevelElement
    {
        public Wall(int y, int x) : base(y, x)
        {
            this.myY = y;
            this.myX = x;
            this.mySprite = '#';
            this.myColor = ConsoleColor.Gray;
        }

    }
}
