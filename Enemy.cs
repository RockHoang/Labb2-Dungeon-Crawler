using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public abstract class Enemy : LevelElement
    {
        public Enemy(int y, int x) : base( y, x)
        { 
        
        }

        public abstract void Update(List<LevelElement> Level);
    }
}
