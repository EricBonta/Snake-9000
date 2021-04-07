using System;
using System.Threading;

namespace Snake_9000
{
    class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';

        /// <summary>
        /// Frameraten är gjorde public för den ändras i andra classer då svårighetsgraden går upp bl.a.
        /// Sedan finns även en bool i Loop() som heter "AiBool". Den avgör om man valt att spela mot en Ai eller inte.
        /// </summary>
        public static int frameRate = 10;

        static void Loop(bool ai)
        {
            // Initialiserar spelet
            // Här skapas våran world och att vi ska rendera saker som finns i world.

            GameWorld world = new GameWorld(60, 40, ai);
            ConsoleRenderer renderer = new ConsoleRenderer(world);

            // Huvudloopen
            // Här i körs alla knapp-läsningar från spelaren och avgör alltså vilken direction man ska åka i.

            while (true)
            {
                // Kom ihåg vad klockan var i början
                DateTime before = DateTime.Now;

                // Hantera knapptryckningar från användaren
                char key = ReadKeyIfExists();
                switch (key)
                {
                    case 'Q':
                        Environment.Exit(0);
                        break;

                    // TODO Lägg till logik för andra knapptryckningar

                    case 'W':
                        if (world.Player.Dir != Direction.Down)
                        {
                            world.Player.Dir = Direction.Up;
                        }

                        break;

                    case 'A':
                        if (world.Player.Dir != Direction.Right)
                        {
                            world.Player.Dir = Direction.Left;
                        }
                        break;

                    case 'S':
                        if (world.Player.Dir != Direction.Up)
                        {
                            world.Player.Dir = Direction.Down;
                        }
                        break;

                    case 'D':
                        if (world.Player.Dir != Direction.Left)
                        {
                            world.Player.Dir = Direction.Right;
                        }
                        break;
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                world.Update();
                renderer.Render();


                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }
            }
        }
        /// <summary>
        /// En meny i starten så man kan välja att spela antingen själv
        /// eller emot en Ai.
        /// Även denna meny som körs när programmet startas om. Därför sätts framerate
        /// tillbaka på 10 och AiBool sätts som antingen true/false.
        /// </summary>
        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\tWelcome to Snake The Game Over 9000!\n");
            Console.WriteLine("[1] To start the game. Press '1'!");
            Console.WriteLine("[2] Play vs SuperInsaneExtremeUnbeatable-Ai. Press '2'!");
            Console.WriteLine("[3] To quit the game. Press '3' or 'Q' while in game!");
            Console.WriteLine("\n _________________________________________________");
            Console.WriteLine("| You win if you get 3 more points than the Ai.   |");
            Console.WriteLine("| The Ai wins if he gets 10 more points than you! |");
            Console.WriteLine("|_________________________________________________|");
            Console.ResetColor();
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                char input = pressedKey.KeyChar;

                switch (input)
                {
                    case '1':
                        Console.Clear();
                        frameRate = 10;
                        Loop(false);
                        break;

                    case '2':
                        Console.Clear();
                        frameRate = 10;
                        Loop(true);
                        break;

                    case '3':
                        Environment.Exit(0);
                        break;
                }
            }
        }
        // Vi startar våran meny, som sedan går vidare till "Loop();"
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
