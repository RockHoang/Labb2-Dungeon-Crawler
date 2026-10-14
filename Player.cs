using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class Player : Enemy
    {
        private string message = " ";
        public Player(int y, int x) : base(y, x)
        {
            this.myY = y;
            this.myX = x;
            this.mySprite = '@';
            this.myColor = ConsoleColor.DarkRed;
            this.myHP = 100;
            this.myAttack = "2d6+2";
            this.myDefence = "2d6+0";
        }

        public override void Update(List<LevelElement> Level) {
            var (posY, posX) = input(this.myY, this.myX);
            ValidMove(Level, posY, posX);
            IsDead();
            
        }

        public string getMessage() { 
            string m = message;
            Console.WriteLine(m.PadRight(50));
            return m;
        }

        private bool ValidMove(List<LevelElement> Level, int y, int x) {
            while (true) {
                foreach (var element in Level)
                {
                    if (element.myX == x && element.myY == y && element is Wall)
                    {
                        message = "You bumped into a wall!";
                        return false;
                    }

                    else if (element.myX == x && element.myY == y && element is Enemy)
                    {
                        combat((Enemy)element);
                        return true;
                    }

                }
                this.myX = x;
                this.myY = y;
                break;
            }

            return true;
        }

        private (int tempY, int tempX) input(int y, int x) {
            ConsoleKeyInfo myInput;
            while (true)
            {
                myInput = Console.ReadKey(true);
                if (myInput.Key == ConsoleKey.UpArrow)
                {
                    y -= 1;
                    message = "You moved up!";
                    break;
                }

                else if (myInput.Key == ConsoleKey.DownArrow)
                {
                    y += 1;
                    message = "You moved down!";
                    break;
                }
                else if (myInput.Key == ConsoleKey.LeftArrow)
                {
                    x -= 1;
                    message = "You moved left!";
                    break;
                }

                else if (myInput.Key == ConsoleKey.RightArrow)
                {
                    x += 1;
                    message = "You moved right!";
                    break;
                }
                else { }
            }
            
            return (y, x);
        }


    }
}
