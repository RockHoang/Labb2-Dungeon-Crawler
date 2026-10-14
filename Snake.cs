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
            this.myHP = 25;
            this.myAttack = new Dice(3, 4, 2);
            this.myDefence = new Dice(1, 8, 5);
        }

        public override void Update(List<LevelElement> Level)
        {
            foreach (LevelElement LevelElement in Level) {
                if (LevelElement is Player p) {
                    AvoidPlayer(p, Level);
                }
            }

        }

        public void AvoidPlayer(Enemy p, List<LevelElement> Level)
        {
            if (DistanceFromPlayer(p.myY, p.myX, this.myY, this.myX) <= 2)
            {
                MoveFromPlayer(p, this.myY, this.myX, Level);
            }
        }
        public void MoveFromPlayer(Enemy p, int y, int x, List<LevelElement> Level) {
            int tempY = y;
            int tempX = x;
            //Console.WriteLine($"Distance move up: {DistanceFromPlayer(pY, pX, tempY-1, x)}");
            //Console.WriteLine($"Distance move down: {DistanceFromPlayer(pY, pX, tempY+1, x)}");
            //Console.WriteLine($"Distance move left: {DistanceFromPlayer(pY, pX, y, tempX-1)}");
            //Console.WriteLine($"Distance move right: {DistanceFromPlayer(pY, pX, y, tempX+1)}");
            if (DistanceFromPlayer(p.myY, p.myX, tempY - 1, x) > 2 && NoCollision(tempY - 1, tempX, Level))
            {
                this.myY = tempY - 1;
            }
            else if (DistanceFromPlayer(p.myY, p.myX, tempY + 1, x) > 2 && NoCollision(tempY + 1, tempX, Level))
            {
                this.myY = tempY + 1;
            }

            else if (DistanceFromPlayer(p.myY, p.myX, y, tempX - 1) > 2 && NoCollision(tempY, tempX - 1, Level))
            {
                this.myX = tempX - 1;
            }

            else if (DistanceFromPlayer(p.myY, p.myX, y, tempX + 1) > 2 && NoCollision(tempY, tempX + 1, Level))
            {
                this.myX = tempX + 1;
            }
            else if (this.myHP > 0)
            {
                FightPlayer(p);
            }
            else { }

        }

        private void FightPlayer(Enemy p) {

            if (p.myY == this.myY-1 && p.myX == this.myX)
            {
                combat(p);
            }
            else if (p.myY == this.myY + 1 && p.myX == this.myX)
            {
                combat(p);
            }

            else if (p.myY == this.myY && p.myX == this.myX - 1)
            {
                combat(p);
            }

            else if (p.myY == this.myY && p.myX == this.myX + 1)
            {
                combat(p);
            }
            else { }
        }

        private bool NoCollision(int y, int x, List<LevelElement> Level) {

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
