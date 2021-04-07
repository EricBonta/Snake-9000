using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Mat som spelaren och Ain kan äta under spelet. Maten är en RenderChar som symboliserar tecknet för den.
    /// Och maten har också en färg och en position.
    /// </summary>
    public class Food : GameObject, IRenderable
    {
        Random rnd = new Random();
        public char RenderChar { get; set; }
        public ConsoleColor RenderColor { get; set; }

        public Food(char renderChar, int x, int y)
            : base(x, y)
        {
            RenderChar = renderChar;
            RandomFoodPosition();
            RenderColor = ConsoleColor.Yellow;
        }
        // Maten får en ny slumpmässigt vald position i consolen. Detta måste ändras om man ändrar storleken på consolen.
        public void RandomFoodPosition()
        {
            Position.X = rnd.Next(2, 59);
            Position.Y = rnd.Next(2, 39);
        }

        public override void Update()
        {
            
        }
    }
}
