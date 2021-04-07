using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Här inne skapas nästan alla object som används i programmet utom 
    /// spelarens och AIns svans.
    /// </summary>
    public class GameWorld
    {
        public List<GameObject> GameObj = new List<GameObject>();

        public Player Player = new Player('►', 30, 20);
        public Food Food = new Food('ð', 0, 0);
        public Ai Ai;

        Random rnd = new Random();
        public int GameWidth;
        public int GameHeight;
        public bool AiBool;

        /// <summary>
        /// Konstruktorn för GameWorld som tar in storleken på consolfönstret som matats in i program.
        /// Här läggs även spelaren och Ain i listan GameObj som sedan renderas i ConsolRenderer.
        /// Skapar också upp en Ai OM AiBool är true. Sedan skapas också upp 4 väggar till att börja med.
        /// </summary>
        public GameWorld(int w, int h, bool aiBool)
        {
            GameWidth = w;
            GameHeight = h;
            AiBool = aiBool;
            GameObj.Add(Player);
            GameObj.Add(Food);
            CreateAi();

            CreateWall();
            CreateWall();
            CreateWall();
            CreateWall();
        }

        public List<Walls> Walls = new List<Walls>();

        // Metod för att skapa Ain om AiBool är true.
        public void CreateAi()
        {
            if (AiBool)
            {
                Ai = new Ai('►', 20, 10);
            }
        }
        // Metoden för att skapa nya väggar.
        // Kollar också då så att en vägg INTE skapas på samma position som en spelare eller Ai.
        public void CreateWall()
        {
            Walls.Add(new Walls('|', 0, 0));

            foreach (Walls w in Walls)
            {
                if (w.Position.ComparePosition(Player))
                {
                    Walls.RemoveAt(Walls.Count - 1);
                    Walls.Add(new Walls('|', 0, 0));

                    if (AiBool)
                    {
                        if (w.Position.ComparePosition(Ai))
                        {
                            Walls.RemoveAt(Walls.Count - 1);
                            Walls.Add(new Walls('|', 0, 0));
                        }
                    }
                }
            }
        }
        // Här kontrolleras ifall en spelare har kört in i en vägg.
        // Eller om man kört in i sin egen svans.
        public void CheckWallsPlayer()
        {
            foreach (Walls wall in Walls)
            {
                if (Player.Position.ComparePosition(wall))
                {
                    GameOver();
                }
            }
            foreach (Tail t in Player.Tail)
            {
                if (Player.Position.ComparePosition(t))
                {
                    GameOver();
                }
            }
        }

        /// <summary>
        /// Här kontrolleras alla olika sätt som en Ai kan vinna eller förlora på.
        /// Ain vinner också om en spelare och en Ai kör rakt in i varandra. (För att öka svårighetsgraden).
        /// Kontrollerar även om en Ai kört in i en vägg, en spelare har kört in i en Ai svans, en Ai kör in i sin egen svans,
        /// en ai kör in i en spelares svans eller om en spelare leder med 3 poäng eller Ain med 10 poäng.
        /// </summary>
        public void GameConditionsAi()
        {
            if (Player.Position.ComparePosition(Ai))
            {
                GameOver();
            }
            foreach (Walls wall in Walls)
            {
                if (Ai.Position.ComparePosition(wall))
                {
                    GameWon();
                }
            }
            foreach (Tail t in Ai.Tail)
            {
                if (Player.Position.ComparePosition(t))
                {
                    GameOver();
                }
                else if (Ai.Position.ComparePosition(t))
                {
                    GameWon();  
                }
            }
            foreach (Tail t in Player.Tail)
            {
                if (Ai.Position.ComparePosition(t))
                {
                    GameWon();
                }
            }
            if (Ai.Poäng + 3 == Player.Poäng)
            {
                GameWon();
            }
            if (Player.Poäng + 10 == Ai.Poäng)
            {
                GameOver();
            }
        }

        /// <summary>
        /// Denna körs då man har förlorat spelet. Denna har lite olika resultet beroende på hur man förlorar.
        /// Men också om man valt att spela ensam eller mot en Ai.
        /// Sedan går den till menyn igen och börjar om programmet ifall man väljer att trycka på "Enter".
        /// </summary>
        private void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 5);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
                 _____   ___  ___  ___ _____ 
                |  __ \ / _ \ |  \/  ||  ___|
                | |  \// /_\ \| .  . || |__  
                | | __ |  _  || |\/| ||  __| 
                | |_\ \| | | || |  | || |___ 
                 \____/\_| |_/\_|  |_/\____/ 
                            ");
            Console.WriteLine(@"
                 _____  _   _ ___________ 
                |  _  || | | |  ___| ___ \
                | | | || | | | |__ | |_/ /
                | | | || | | |  __||    / 
                \ \_/ /\ \_/ / |___| |\ \ 
                 \___/  \___/\____/\_| \_|
                                ");
            if (AiBool)
            {
                if (Player.Poäng + 10 == Ai.Poäng)
                {
                    Console.WriteLine("\tThe Ai got 10 more points than you!");
                }
                else
                {
                    Console.WriteLine("\tDont hit the walls or the Ai!");
                }
            }
            else
            {
                Console.WriteLine("\tDont hit the walls!");
            }
            Console.WriteLine("\tPress 'Enter' to play again, press 'Q' to quit.");

            Console.Write($"\tYou got ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Player.Poäng);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" points.");

            if (AiBool)
            {
                Console.Write($"\tAi got ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Ai.Poäng);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" points.");
            }

            AiBool = false;
            Console.ResetColor();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Program.Menu();
                    break;
                }
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                }
            }
        }
        /// <summary>
        /// Denna körs då man har vunnit spelet men har också några olika utfall beroende på hur man vinner.
        /// Men också om man valt att spela ensam eller mot en Ai.
        /// Sedan går den till menyn igen och börjar om programmet ifall man väljer att trycka på "Enter".
        /// </summary>
        private void GameWon()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 5);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
            __   __            _    _             _ 
            \ \ / /           | |  | |           | |
             \ V /___  _   _  | |  | | ___  _ __ | |
              \ // _ \| | | | | |/\| |/ _ \| '_ \| |
              | | (_) | |_| | \  /\  / (_) | | | |_|
              \_/\___/ \__,_|  \/  \/ \___/|_| |_(_)
                                        
                                ");

            if (Ai.Poäng + 3 == Player.Poäng)
            {
                Console.WriteLine("\tYou got 3 more score than the Ai!");
            }
            else
            {
                Console.WriteLine("\tThe Ai hit a wall or you!");
            }
            Console.WriteLine("\tPress 'Enter' to play again, press 'Q' to quit.");

            Console.Write($"\tYou got ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Player.Poäng);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" points.");

            Console.Write($"\tAi got ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Ai.Poäng);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" points.");

            AiBool = false;
            Console.ResetColor();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Program.Menu();
                    break;
                }
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                }
            }
        }
        /// <summary>
        /// Här kontrolleras ifall en spelare har kört på en bit mat.
        /// Då får man ett poäng och maten får en ny slumpvald position.
        /// Kontrollerar också så att maten in skapas i en vägg eller i en spelares position.
        /// Spelaren får också ett poäng och frameRaten ökas! När spelarens poäng går att göra % 5 så skapas även en till vägg.
        /// Detta för att öka svårighetsgraden.
        /// </summary>
        public void EatFoodPlayer()
        {
            if (Player.Position.ComparePosition(Food))
            {
                Player.AddTail();

                GameObj.Remove(Food);
                Food.RandomFoodPosition();

                foreach (Walls w in Walls)
                {
                    while (Food.Position.ComparePosition(w))
                    {
                        Food.RandomFoodPosition();
                    }
                }
                while (Player.Position.ComparePosition(Food))
                {
                    Food.RandomFoodPosition();
                }

                GameObj.Add(Food);

                Player.Poäng++;
                Program.frameRate++;

                if (Player.Poäng % 5 == 0)
                {
                    CreateWall();
                }
            }
        }
        /// <summary>
        /// Samma sak som innan fast vi kollar om en Ai har ätit en mat istället.
        /// Samma villkor med svårighetsgraden här.
        /// </summary>
        public void EatFoodAi()
        {
            if (Ai.Position.ComparePosition(Food))
            {
                Ai.AddTail();

                GameObj.Remove(Food);
                Food.RandomFoodPosition();
                GameObj.Add(Food);

                foreach (Walls w in Walls)
                {
                    while (Ai.Position.ComparePosition(w))
                    {
                        Food.RandomFoodPosition();
                    }
                }
                while (Ai.Position.ComparePosition(Food))
                {
                    Food.RandomFoodPosition();
                }

                Ai.Poäng++;
                Program.frameRate++;

                if (Ai.Poäng % 5 == 0)
                {
                    CreateWall();
                }
            }
        }
        /// <summary>
        /// Detta är funktionen som kollar ifall en spelare har åkt utanför consolen.
        /// Då flyttas han till andra sidan banan, som ett sätta att kunna "korsa" banan.
        /// </summary>
        public void PlayerMovingThroughEdge()
        {
            if (Player.Position.X == 1 && Player.Dir == Direction.Left)
            {
                Player.Position.X = GameWidth - 1;
            }
            else if (Player.Position.X == GameWidth - 1 && Player.Dir == Direction.Right)
            {
                Player.Position.X = 1;
            }
            else if (Player.Position.Y == 1 && Player.Dir == Direction.Up)
            {
                Player.Position.Y = GameHeight - 1;
            }
            else if (Player.Position.Y == GameHeight - 1 && Player.Dir == Direction.Down)
            {
                Player.Position.Y = 1;
            }
        }
        /// <summary>
        /// Samma sak fast ifall en Ai kört ut från kanten av consolen.
        /// </summary>
        public void AiMovingThroughWalls()
        {
            if (Ai.Position.X == 1 && Ai.Dir == Direction.Left)
            {
                Ai.Position.X = GameWidth - 1;
            }
            else if (Ai.Position.X == GameWidth - 1 && Ai.Dir == Direction.Right)
            {
                Ai.Position.X = 1;
            }
            else if (Ai.Position.Y == 1 && Ai.Dir == Direction.Up)
            {
                Ai.Position.Y = GameHeight - 1;
            }
            else if (Ai.Position.Y == GameHeight - 1 && Ai.Dir == Direction.Down)
            {
                Ai.Position.Y = 1;
            }
        }
        /// <summary>
        /// Här bestäms att när en spelare eller en Ai har mer än 3 poäng som kommer maten slumpmässigt att byta position.
        /// Också ett sätt att öka svårighetsgraden.
        /// </summary>
        public void HarderDifficulty()
        {
            if (Player.Poäng > 3)
            {
                int rndnmr = rnd.Next(1, 200);
                if (rndnmr == 1)
                {
                    Food.RandomFoodPosition();
                }
            }
            else if (AiBool && Ai.Poäng > 3)
            {
                int rndnmr = rnd.Next(1, 200);
                if (rndnmr == 1)
                {
                    Food.RandomFoodPosition();
                }
            }
        }
        // Använder denna som test metod, då den övre har en slumpfaktor som var svår att testa med randomNummer.
        public void HarderDifficultyTEST()
        {
            if (Player.Poäng > 3)
            {
                int rndnmr = 1;
                if (rndnmr == 1)
                {
                    Food.RandomFoodPosition();
                }
            }
            else if (AiBool && Ai.Poäng > 3)
            {
                int rndnmr = 1;
                if (rndnmr == 1)
                {
                    Food.RandomFoodPosition();
                }
            }
        }
        /// <summary>
        /// Här körs alla metoder ifrån GameWorld.
        /// Ai metoderna körs enbart om AiBool är true. Där av egna metoder.
        /// </summary>
        public void Update()
        {
            CheckWallsPlayer();
            EatFoodPlayer();
            PlayerMovingThroughEdge();
            HarderDifficulty();
            Player.Update();

            if (AiBool)
            {
                GameConditionsAi();
                EatFoodAi();
                AiMovingThroughWalls();
                Ai.FoodUpdate(Food, Walls);
            }
        }
    }
}
