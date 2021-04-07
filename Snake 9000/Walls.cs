using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Classen för väggarna, de har en symbol/char för att visa hur de ser ut, en färg och en position.
    /// </summary>
    public class Walls : GameObject, IRenderable
    {
        Random rnd = new Random();
        public char RenderChar { get; set; }

        public ConsoleColor RenderColor { get; set; }

        public Walls(char renderChar, int x, int y)
            : base(x, y)
        {
            RenderChar = renderChar;
            Update();
            RenderColor = ConsoleColor.White;
        }
        // Väggarnas position slumpas alltid ut och detta värde måste ändras precis som matens ifall man 
        // ändrar storlek på consolfönstret. I gameworld kollas även så att en vägg inte placeras innuti ett annat object.
        public override void Update()
        {
            Position.X = rnd.Next(2, 59);
            Position.Y = rnd.Next(2, 39);
        }
    }

}
