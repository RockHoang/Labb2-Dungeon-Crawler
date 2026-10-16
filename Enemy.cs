using System;
using System.Collections.Generic;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public abstract class Enemy : LevelElement
    {
        public int myHP { get; set; }

        public Dice myAttack { get; set; }
        public Dice myDefence { get; set; }
       
        public Enemy(int y, int x) : base( y, x)
        { 
        
        }

        public abstract void Update(List<LevelElement> Level);

        public void combat(Enemy myEnemy)
        {
            string summary = "";
            int attack = this.myAttack.Throw();
            int defence = myEnemy.myDefence.Throw();
            int outcome = attack - defence;
            
            summary += $"{this.GetType().Name} Started combat with {myEnemy.GetType().Name}\n" + 
                $"{this.GetType().Name} threw {myAttack} got:{attack} {myEnemy.GetType().Name} threw {myEnemy.myDefence} got:{defence}      \n";
            if (outcome > 0) {
                int enemyHP = myEnemy.myHP;
                myEnemy.myHP -= outcome;
                summary += $"{this.GetType().Name} did {outcome} damage to {myEnemy.GetType().Name} HP {enemyHP} -> {myEnemy.myHP}".PadRight(60);
                Console.WriteLine(summary);
            }
            else {
                summary += $"{this.GetType().Name} did no damage to {myEnemy.GetType().Name}! " +
                    $"{myEnemy.GetType().Name} counterattacked {this.GetType().Name}!\n";
                CounterAttack(myEnemy, summary);
                
            }

        }

        public void CounterAttack(Enemy myEnemy, string summary) {
            int attack = myEnemy.myAttack.Throw();
            int defence = this.myDefence.Throw();
            int outcome = attack - defence;
           
            summary += $"{myEnemy.GetType().Name} threw {myEnemy.myAttack} got:{attack} {this.GetType().Name} threw {this.myDefence} got:{defence}\n";
            if (outcome > 0)
            {
                int enemyHP = this.myHP;
                this.myHP -= outcome;
                summary += $"{myEnemy.GetType().Name} did {outcome} damage to {this.GetType().Name} HP {enemyHP} -> {this.myHP}".PadRight(60);

            }
            else
            {
                summary += $"{myEnemy.GetType().Name} did no damage to {this.GetType().Name}!".PadRight(60);
            }
            Console.WriteLine(summary);
        }

        public bool IsDead() {
            if (myHP <= 0)
            {
                string message = $"{this.GetType().Name} have fallen!";
                Console.WriteLine(message.PadRight(60));

                return true;
            }
            else {
                return false;
            }
        }
    }
}
