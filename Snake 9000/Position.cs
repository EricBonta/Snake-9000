using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    //enum Operators { Plus, Minus, Equal, NotEqual };

    /// <summary>
    /// Tar in en position av ett object där X och Y markerar dess position på skärmen.
    /// </summary>
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        // 2 olika metoder att jämföras objects positioner 
        // en för gameobject och en för en position som jag använder i min Ai.
        public bool ComparePosition(GameObject gameobj)
        {
            if (X == gameobj.Position.X && Y == gameobj.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ComparePosition(Position gameobj)
        {
            if (X == gameobj.X && Y == gameobj.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
