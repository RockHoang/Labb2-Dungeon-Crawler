using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public abstract class LevelElement
    {
        public LevelElement(int y, int x) {
            this.myY = y;
            this.myX = x;
        
        }
        

        public int myY { get; set; }
        public int myX { get; set; }

        public char mySprite { get; set; }
        public ConsoleColor myColor { get; set; }

        public void Draw() { 
            Console.ForegroundColor = myColor;
            Console.Write(mySprite);
            Console.ResetColor();
        }

    }
}
