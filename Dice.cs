using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class Dice
    {
        private int numberOfDice = 0;
        private int sidesPerDice = 0;
        private int modifier = 0;

        private string myDiceStats = "";

        public Dice(int nD, int sPD, int m) {

            numberOfDice = nD;
            sidesPerDice = sPD;
            modifier = m;
            myDiceStats = $"{nD}d{sPD}+{m}";
        }

        public int Throw(){ 

            Random rand = new Random();
            int sum = 0;
            for (int i = 0; i < numberOfDice; i++) {
                sum += rand.Next(1, (sidesPerDice + 1));
            }

            return sum+modifier; 
        }

        public override string ToString()
        {
            return myDiceStats;
        }


    }
}
