using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class Dice
    {
        private int timesThrown = 0;
        private int dValue = 0;
        private int plusValue = 0;

        public Dice(string stats) {
            timesThrown = 0;
            dValue = 0;
            plusValue = 0;
        }

        public int Throw(){ 

            Random rand = new Random();

            return rand.Next(dValue) + plusValue; 
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
