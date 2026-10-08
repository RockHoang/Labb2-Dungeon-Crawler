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
                    int d = DistanceFromPlayer(p.myY, p.myX, this.myY, this.myX);
                    AvoidPlayer(d);
                }
            }

        }

        public void AvoidPlayer(int distance)
        {
            if (distance == 1)
            {
                MoveFromPlayer(distance, myY, myX);
            }
        }
        public void MoveFromPlayer(int distance, int y, int x) {
            
        }

        private (int newX, int newY) move(int x, int y) {
            Random rand = new Random();
            int snakeMove = rand.Next(4);

            switch (snakeMove)
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

        public int DistanceFromPlayer(int pY, int pX, int snakeY, int snakeX)
        {
            double Ys = Math.Pow(Convert.ToDouble(snakeY - pY), 2);
            double Xs = Math.Pow(Convert.ToDouble(snakeX - pX), 2);
            double distance = Math.Round(Math.Sqrt(Ys+Xs));
            return (int)distance;
        }
    }
}
