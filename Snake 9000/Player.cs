using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Spelar classen, summeras ihop till alla olika delar och attributer som spelaren har.
    /// Detta fall en svans när den får poäng, poäng, ett tecken som symboliserar den, en direction som den åker åt
    // , en färg och en position.
    /// </summary>
    public class Player : GameObject, IRenderable, IMoveable
    {
        public List<Tail> Tail = new List<Tail>();
        public int Poäng { get; set; }
        public char RenderChar { get; set; }
        public Direction Dir { get; set; }
        public ConsoleColor RenderColor { get; set; }

        public Player(char renderChar, int x, int y)
            :base (x,y)
        {
            Dir = Direction.None;
            RenderChar = renderChar;
            RenderColor = ConsoleColor.Red;
        }
        // Metod för att lägga till en svans då spelaren får poäng.
        public void AddTail()
        {
            Tail.Add(new Tail('o', 0, 0, ConsoleColor.Red));
        }
        // Här uppdateras spelarens position efter spelarens direction varje gång programmet uppdateras
        // lägger även till en svans när spelaren får poäng, svansen är i en lista.
        public override void Update()
        {
            int playerPrevY = Position.Y;
            int playerPrevX = Position.X;
            switch (Dir)
            {
                case Direction.Up:
                    Position.Y -= 1;
                    RenderChar = '▲';
                    break;

                case Direction.Down:
                    Position.Y += 1;
                    RenderChar = '▼';
                    break;

                case Direction.Left:
                    Position.X -= 1;
                    RenderChar = '◄';
                    break;

                case Direction.Right:
                    Position.X += 1;
                    RenderChar = '►';
                    break;

                case Direction.None:
                    break;
            }

            // Eftersom den sista svansen ersätts med den första så kommer det se ut som att svansen
            // följer efter spelaren när spelaren rör på sig. Den tar då spelarens senaste position
            // och placerar svansen där.
            if (Tail.Count > 0)
            {
                Tail tailEnd = Tail[Tail.Count - 1];
                tailEnd.Position.X = playerPrevX;
                tailEnd.Position.Y = playerPrevY;
                Tail.Remove(tailEnd);
                Tail.Insert(0, tailEnd);
            }
        }
    }
}