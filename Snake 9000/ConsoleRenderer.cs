using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Detta är den classen som renderar allt som ska vara i consolen, object osv.
    /// </summary>
    class ConsoleRenderer
    {
        public GameWorld World { get; }
        public ConsoleRenderer(GameWorld gameWorld)
        {
            // Skapar upp consolfönstret efter den storleken jag har satt i Program.
            World = gameWorld;
            Console.SetWindowSize(World.GameWidth, World.GameHeight);
            Console.SetBufferSize(World.GameWidth, World.GameHeight);
        }

        /// <summary>
        /// Här så kör vi en tomrad efter varje object som har renderats istället för att köra
        /// console.clear. Detta för att få bort att det "blinkar" i fönstret.
        /// </summary>
        public void RenderBlank()
        {
            foreach (var item in World.GameObj)
            {
                Console.SetCursorPosition(item.Position.X, item.Position.Y);
                Console.Write(" ");
            }

            foreach (var item in World.Player.Tail)
            {
                Console.SetCursorPosition(item.Position.X, item.Position.Y);
                Console.Write(" ");
            }

            if (World.AiBool)
            {
                Console.SetCursorPosition(World.Ai.Position.X, World.Ai.Position.Y);
                Console.Write(" ");

                foreach (var item in World.Ai.Tail)
                {
                    Console.SetCursorPosition(item.Position.X, item.Position.Y);
                    Console.Write(" ");

                }
            }
        }

        /// <summary>
        /// Här renderas varje object ut som ska vara med i consolen. 
        /// Lite beroende på om Ai-Bool är true eller inte.
        /// </summary>
        public void Render()
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Player Score: {World.Player.Poäng}");

            if (World.AiBool)
            {
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ai Score: {World.Ai.Poäng}");
                Console.ResetColor();
            }

            foreach (var wall in World.Walls)
            {
                Console.ForegroundColor = wall.RenderColor;
                Console.SetCursorPosition(wall.Position.X, wall.Position.Y);
                Console.Write(wall.RenderChar);
            }

            foreach (var item in World.GameObj)
            {
                Console.ForegroundColor = (item as IRenderable).RenderColor;
                Console.SetCursorPosition(item.Position.X, item.Position.Y);
                Console.Write((item as IRenderable)?.RenderChar);
            }

            foreach (var t in World.Player.Tail)
            {
                Console.ForegroundColor = t.RenderColor;
                Console.SetCursorPosition(t.Position.X, t.Position.Y);
                Console.Write(t.RenderChar);
            }

            if (World.AiBool)
            {
                Console.ForegroundColor = World.Ai.RenderColor;
                Console.SetCursorPosition(World.Ai.Position.X, World.Ai.Position.Y);
                Console.Write(World.Ai.RenderChar);

                foreach (var t in World.Ai.Tail)
                {
                    Console.ForegroundColor = t.RenderColor;
                    Console.SetCursorPosition(t.Position.X, t.Position.Y);
                    Console.Write(t.RenderChar);
                }
            }
        }
    }
}
