using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class Rat : Enemy
    {
        public Rat(int y, int x) : base(y, x)
        {
            this.myY = y;
            this.myX = x;
            this.mySprite = 'r';
            this.myColor = ConsoleColor.Red;
        }

        public override void Update(List<LevelElement> Level)
        {
            while (true)
            {
                var (posX, posY) = move(this.myX, this.myY);
                if (ValidMove(Level, posX, posY))
                {
                    break;
                }
            }
            
            

        }

        private bool ValidMove(List<LevelElement> Level, int x, int y)
        {
            foreach (var element in Level) {
                if (element.myX == x && element.myY == y) { 
                    return false;
                }
            
            }
            this.myX = x;
            this.myY= y;
            return true;
        }

        private (int newX, int newY) move(int x, int y) {
            Random rand = new Random();

            int move = rand.Next(4);

            switch (move)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    y++;
                    break;
                case 2:
                    x--;
                    break;
                case 3:
                    x++;
                    break;

            }
            return (x, y);

        }
    }
}
