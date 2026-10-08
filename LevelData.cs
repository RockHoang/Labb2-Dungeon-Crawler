using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class LevelData
    {
        List<LevelElement> myLevel = new List<LevelElement>();
        List<Wall> wallHistory = new List<Wall>();
        int maxY = 0;
        int maxX = 0;

        int maxVision = 5;

        public void InitalizingMap() {
            string myPath = Path.Combine(AppContext.BaseDirectory, "Level1.txt");
            StreamReader levelReader = new StreamReader(myPath);

            List<string> map = new List<string>();
            
           

            string myline = levelReader.ReadLine();
            while (myline != null)
            {
                map.Add(myline);
                myline = levelReader.ReadLine();
            }
            levelReader.Close();

            maxY = map.Count;
            maxX = 0;
            for (int i = 0; i < map.Count; i++) {
                if (map[i].Length > maxX) {
                    maxX = map[i].Length;
                }
            }

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {

                    if (map[i][j].Equals('#'))
                    {
                        myLevel.Add(new Wall(i, j));
                    }

                    else if (map[i][j].Equals('r'))
                    {
                        myLevel.Add(new Rat(i, j));
                    }

                    else if (map[i][j].Equals('s'))
                    {
                        myLevel.Add(new Snake(i, j));
                    }

                    else if (map[i][j].Equals('@'))
                    {
                        myLevel.Add(new Player(i, j));
                    }

                    else { 
                    
                    }
                }
            }

            DrawLevel(myLevel, maxY, maxX, maxVision);
        }

        void DrawLevel(List<LevelElement> Level, int maxY, int maxX, int maxVision) {
           
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            

            for (int i = 0; i < maxY; i++) {
                for (int j = 0; j < maxX; j++) {
                    if (!DrawElement(Level, i, j, maxVision)) {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            foreach (var element in Level) {

                if (element is Player p) {
                    
                    Console.WriteLine($"Event:");
                    string m = p.getMessage();
                }
            
            }

            
            
        }

        bool SeenWall(int y, int x) {

            if (wallHistory.Count == 0) { 
                return false;
            }

            foreach (var element in wallHistory)
            {
                if (element.myY == y && element.myX == x) { 
                    return true;
                }
            }
            return false;
        }

        void RecordSeenWalls(LevelElement element) {
            if (!SeenWall(element.myY, element.myX) && element is Wall)
            {
                wallHistory.Add((Wall)element);
            }

        }

        //beräknar vad spelaren ser med pythagoras sats.
        int PlayerVision(int playerY, int playerX, LevelElement element)
        {
            double Ys = Math.Pow(Convert.ToDouble(playerY - element.myY), 2);
            double Xs = Math.Pow(Convert.ToDouble(playerX - element.myX), 2);
            double distansce = Math.Sqrt(Ys+Xs);
            distansce = Math.Round(distansce);

            return (int)distansce;
        }

        bool DrawElement(List<LevelElement> level, int y, int x, int maxVision) {
            int pY = 0;
            int pX = 0;
            foreach (var element in level) {
                if (element is Player p) {
                    pY = p.myY;
                    pX = p.myX;
                }
            }

            for (int i = 0; i < level.Count; i++) {
                
                if (level[i].myY == y && level[i].myX == x && (PlayerVision(pY, pX, level[i]) <= maxVision || SeenWall(level[i].myY, level[i].myX)))
                {
                    level[i].Draw();
                    RecordSeenWalls(level[i]);
                    return true;
                }
            }
            return false;
        }

        public void UpdateGane() {
            while (true)
            {
                PlayerTurn();
                
                EnemyTurn();
                //Thread.Sleep(100);
                DrawLevel(myLevel, maxY, maxX, maxVision);
                //Console.WriteLine($"Recorded Walls: {wallHistory.Count}");

            }
        }

        private void PlayerTurn() {
            foreach (var element in myLevel) {
                if (element is Player p) {
                    p.Update(myLevel);
                }
            }

        }

        private void EnemyTurn()
        {
            foreach (var element in myLevel)
            {
                if (element is Rat r)
                {
                    r.Update(myLevel);
                }
                else if (element is Snake s) { 
                    s.Update(myLevel);
                }
            }

        }

    }
}
