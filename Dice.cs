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

        private string myDiceStats = "";

        public Dice(string stats) {
            myDiceStats = stats;
            string value = stats[0].ToString();
            int tvalue = Convert.ToInt32(stats[0].ToString());
            timesThrown = tvalue;
            dValue = FindDiceSides(stats);
            plusValue = FindPlusValue(stats);
        }

        public int Throw(){ 

            Random rand = new Random();
            int sum = 0;
            for (int i = 0; i < timesThrown; i++) {
                sum += rand.Next(1, (dValue + 1));
            }

            return sum+plusValue; 
        }

        public override string ToString()
        {
            return myDiceStats;
        }

        private int FindDiceSides(String s) {
            int start = 0;
            int end = 0;
            string sides = "";

            for (int i = 0; i < s.Length; i++) {
                if (s[i].Equals('d')) {
                    start = i;
                }
                else if (s[i].Equals('+')) { 
                    end = i;
                }
            }

            for(int i = start+1; i < end; i++) {
                sides += s[i].ToString();
            }


            return Convert.ToInt32(sides);
        }

        private int FindPlusValue(String s)
        {
            int plus = 0;
            string plusValue = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Equals('+'))
                {
                    plus = i;
                }
            }

            for (int i = plus; i < s.Length; i++)
            {
                plusValue = plusValue + s[i].ToString();
            }


            return Convert.ToInt32(plusValue);
        }

    }
}
