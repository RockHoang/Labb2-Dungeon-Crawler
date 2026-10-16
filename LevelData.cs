using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Labb2_Dungeon_Crawler
{
    public class LevelData
    {
        private readonly List<LevelElement> gameLevel = new List<LevelElement>();
        private List<Wall> wallHistory = new List<Wall>();
        int maxY = 0;
        int maxX = 0;

        int maxVision = 5;
        int turns = 0;
        bool endgame=false;

        public List<LevelElement> Elements
        {
            get => gameLevel;
        }

        public void Load(string filename) {
            string myPath = Path.Combine(AppContext.BaseDirectory, filename);
            StreamReader levelReader = new StreamReader(myPath);

            List<string> map = new List<string>();
            
           
            string myline = levelReader.ReadLine();
            while (myline != null)
            {
                if (!myline.IsWhiteSpace()) {
                    map.Add(myline);
                }
                
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
                        Elements.Add(new Wall(i, j));
                    }

                    else if (map[i][j].Equals('r'))
                    {
                        Elements.Add(new Rat(i, j));
                    }

                    else if (map[i][j].Equals('s'))
                    {
                        Elements.Add(new Snake(i, j));
                    }

                    else if (map[i][j].Equals('@'))
                    {
                        Elements.Add(new Player(i, j));
                    }

                    else { 
                    
                    }
                }
            }

            DrawLevel(Elements, maxY, maxX, maxVision);
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
            Console.WriteLine($"Turn: {turns} Use the Arrowkeys to move, press Esc to end the game.".PadRight(60));
            
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

        //Beräknar vad spelaren ser med pythagoras sats.
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

        public void UpdateGame() {

            while (!endgame)
            {
      
                PlayerTurn();
                RemoveTheDead();
                EnemyTurn();
                RemoveTheDead();
                turns++;
                DrawLevel(Elements, maxY, maxX, maxVision);
            }
            
        }

        private void PlayerTurn() {

            foreach (var element in Elements) {
                if (element is Player p) {
                    p.Update(Elements);
                }
            }

        }

        private void EnemyTurn()
        {
            foreach (var element in Elements)
            {
                if (element is Rat r)
                {
                    r.Update(Elements);
                }
                else if (element is Snake s) { 
                    s.Update(Elements);
                }
            }

        }

        private void RemoveTheDead() {
            
            foreach (var element in Elements) {
                if (element is Rat r && r.IsDead())
                {
                    Elements.Remove(r);
                    DrawLevel(Elements, maxY, maxX, maxVision);
                    break;
                }
                else if (element is Snake s && s.IsDead())
                {
                    Elements.Remove(s);
                    DrawLevel(Elements, maxY, maxX, maxVision);
                    break;
                }
                else if (element is Player p && p.IsDead()) 
                {
                    Elements.Remove(p);
                    endgame = true;
                    Console.WriteLine("Press any key to end the game.".PadRight(60));
                    DrawLevel(Elements, maxY, maxX, maxVision);
                    Console.ReadKey();
                    break;
                }
            }
        }

    }
}
