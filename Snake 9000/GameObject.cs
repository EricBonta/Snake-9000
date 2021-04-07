using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// En class för att summera alla Spelobjecten. Dessa har en position i consolen.
    /// </summary>
   public abstract class GameObject
    {
        public Position Position;
        public GameObject(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public abstract void Update();
    }
}
