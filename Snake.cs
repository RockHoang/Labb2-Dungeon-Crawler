using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Labb2_Dungeon_Crawler
{
    public class Snake : Enemy
    {
        public Snake(int y, int x) : base(y, x)
        {
            this.myY = y;
            this.myX = x;
            this.mySprite = 's';
            this.myColor = ConsoleColor.Green;
        }

        public override void Update(List<LevelElement> Level)
        {
            foreach (LevelElement LevelElement in Level) {
                if (LevelElement is Player p) {
                    AvoidPlayer(p.myY, p.myX, Level);
                }
            }

        }

        public void AvoidPlayer(int pY, int pX, List<LevelElement> Level)
        {
            if (DistanceFromPlayer(pY, pX, this.myY, this.myX) == 1)
            {
                MoveFromPlayer(pY, pX, this.myY, this.myX, Level);
            }
        }
        public void MoveFromPlayer(int pY, int pX, int y, int x, List<LevelElement> Level) {
            int tempY = y;
            int tempX = x;
            //Console.WriteLine($"Distance move up: {DistanceFromPlayer(pY, pX, tempY-1, x)}");
            //Console.WriteLine($"Distance move down: {DistanceFromPlayer(pY, pX, tempY+1, x)}");
            //Console.WriteLine($"Distance move left: {DistanceFromPlayer(pY, pX, y, tempX-1)}");
            //Console.WriteLine($"Distance move right: {DistanceFromPlayer(pY, pX, y, tempX+1)}");
            if (DistanceFromPlayer(pY, pX, tempY-1, x) > 1 && Collision(tempY-1, tempX, Level))
            {
                this.myY = tempY-1;
            }
            else if (DistanceFromPlayer(pY, pX, tempY+1, x) > 1 && Collision(tempY+1, tempX, Level))
            {
                this.myY = tempY+1;
            }

            else if (DistanceFromPlayer(pY, pX, y, tempX-1) > 1 && Collision(tempY, tempX-1, Level))
            {
                this.myX = tempX-1;
            }

            else if (DistanceFromPlayer(pY, pX, y, tempX+1) > 1 && Collision(tempY, tempX+1, Level))
            {
                this.myX = tempX+1;
            }
            else {
                Console.WriteLine("Cannot move");
            }

        }

        private bool Collision(int y, int x, List<LevelElement> Level) {

            foreach (LevelElement element in Level) {
                if (element.myY == y && element.myX == x) {
                    return false;
                }
            }
            return true;
        }

        public int DistanceFromPlayer(int pY, int pX, int snakeY, int snakeX)
        {
            double Ys = Math.Pow(Convert.ToDouble(snakeY - pY), 2);
            double Xs = Math.Pow(Convert.ToDouble(snakeX - pX), 2);
            double distance = Math.Round(Math.Sqrt(Ys+Xs));
            return (int)distance;
        }
    }
}
