using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public abstract class Enemy : LevelElement
    {
        public int myHP { get; set; }

        public string myAttack { get; set; }
        public string myDefence { get; set; }
       
        public Enemy(int y, int x) : base( y, x)
        { 
        
        }

        public abstract void Update(List<LevelElement> Level);

        public void combat(Enemy myEnemy)
        {
            Dice attackeDice = new Dice(myAttack);
            Dice defenceDice = new Dice(myEnemy.myDefence);

            int attack = attackeDice.Throw();
            int defence = defenceDice.Throw();
            int outcome = attack - defence;
            Console.WriteLine($"{this.GetType().Name} Started combat with {myEnemy.GetType().Name}".PadRight(60));
            Console.WriteLine($"{this.GetType().Name} threw {myAttack} got:{attack} {myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}".PadRight(60));
            if (outcome > 0) {
                int enemyHP = myEnemy.myHP;
                myEnemy.myHP -= outcome;
               //Console.WriteLine($"{this.GetType().Name} threw {myAttack} got:{attack} {myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}".PadRight(60));
               // Console.WriteLine($"{myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}".PadRight(60));
                Console.WriteLine($"{this.GetType().Name} did {outcome} damage to {myEnemy.GetType().Name} HP {enemyHP} -> {myEnemy.myHP}".PadRight(60));

            }
            else {
                //Console.WriteLine($"{this.GetType().Name} threw {myAttack} got:{attack} {myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}".PadRight(60));
                //Console.WriteLine($"{myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}".PadRight(60));
                Console.WriteLine($"{this.GetType().Name} did no damage to {myEnemy.GetType().Name}!".PadRight(60));
            }

        }

        public bool IsDead() {
            if (myHP <= 0)
            {
                string message = $"{this.GetType().Name} have fallen!";
                Console.WriteLine(message.PadRight(60));

                return true;
            }
            else {
                //Console.WriteLine(" ".PadRight(60));
                return false;
            }
        }
    }
}
