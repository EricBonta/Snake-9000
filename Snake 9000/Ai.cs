using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Classen för Ain som man kan spela mot. Ain har i stort sätt alla saker som en spelare har, svans, poäng,
    /// en symbol/tecken, en direction, en färg och en position.
    /// </summary>
    public class Ai : GameObject, IRenderable, IMoveable
    {
        public List<Tail> Tail = new List<Tail>();
        Random rnd = new Random();
        public int Poäng { get; set; }
        public char RenderChar { get; set; }
        public Direction Dir { get; set; }
        public ConsoleColor RenderColor { get; set; }

        public Ai(char renderChar, int x, int y)
            : base(x, y)
        {
            Dir = Direction.Up;
            RenderChar = renderChar;
            RenderColor = ConsoleColor.Cyan;
        }
        // Metod för Ain att lägga till en svans då den tar poäng, fungerar precis som spelarens.
        public void AddTail()
        {
            Tail.Add(new Tail('o', 0, 0, ConsoleColor.Cyan));
        }

        public int CalculcateDistance(int a, int b)
        {
            if (a > b)
            {
                return a - b;
            }
            return b - a;
        }
        /// <summary>
        /// Ain kommer att leta efter maten och jämföra sin position med dens.
        /// Sedan kommer den att åka åt det hållet maten är åt. Med X-led höger som prio.
        /// </summary>
        public void AiFindingFood(GameObject food)
        {
            int foodPositionX = food.Position.X;
            int foodPositionY = food.Position.Y;

            int posX = CalculcateDistance(foodPositionX, Position.X);
            int posY = CalculcateDistance(foodPositionY, Position.Y);

            if (posX > posY)
            {
                if (foodPositionX > Position.X)
                {
                    if (Dir != Direction.Left)
                    {
                        Dir = Direction.Right;
                    }

                }
                else if (foodPositionX < Position.X)
                {
                    if (Dir != Direction.Right)
                    {
                        Dir = Direction.Left;
                    }
                }
            }
            else
            {
                if (foodPositionY > Position.Y)
                {
                    if (Dir != Direction.Up)
                    {
                        Dir = Direction.Down;
                    }
                }

                else if (foodPositionY < Position.Y)
                {
                    if (Dir != Direction.Down)
                    {
                        Dir = Direction.Up;
                    }
                }
            }
        }
        // Metod för att kolla om ain har en vägg jämfört med den position man matar in.
        // Min tanke först var att AIn skulle jämföra sig med alla saker som var runt om den hela tiden
        // och på så sätt alltid kunna undvika väggar. Men jag ville att den skulle kunna "misslyckas".
        // Där av har jag inte implenterat den logiken.
        public bool CheckForWalls(List<Walls> wall, Position position)
        {
            foreach (Walls w in wall)
            {
                if (w.Position.ComparePosition(position))
                {
                    return true;
                }
            }
            return false;
        }

        // AIn underviker bara väggar i X led och inte Y led.
        // Där av är den inte helt perfekt utan kan fortfarande köra in i väggarna.
        // Den slumpar också om den ska åka upp eller ner ifrån en vägg, för att få en viss slumpfaktor.
        public void AiAvoidingWalls(List<Walls> wall)
        {
            if (Dir == Direction.Right)
            {
                if (CheckForWalls(wall, new Position(Position.X +1, Position.Y)))
                {
                    int rndNmr = rnd.Next(1, 3);
                    if (rndNmr == 1 && CheckForWalls(wall, new Position(Position.X, Position.Y - 1)))
                    {
                        Dir = Direction.Up;
                    }
                    else if(rndNmr == 2 && CheckForWalls(wall, new Position(Position.X, Position.Y + 1)))
                    {
                        Dir = Direction.Down;
                    }
                    else
                    {
                        Dir = Direction.Up;
                    }
                }
            }
            else 
            {
                if (CheckForWalls(wall, new Position(Position.X - 1, Position.Y)))
                {
                    int rndNmr = rnd.Next(1, 3);
                    if (rndNmr == 1 && CheckForWalls(wall, new Position(Position.X, Position.Y - 1)))
                    {
                        Dir = Direction.Up;
                    }
                    else if (rndNmr == 2 && CheckForWalls(wall, new Position(Position.X, Position.Y + 1)))
                    {
                        Dir = Direction.Down;
                    }
                    else
                    {
                        Dir = Direction.Up;
                    }
                }
            }
        }
        /// <summary>
        /// Här körs ovanstående metoder för att få fram vart Ain ska vända sig.
        /// Sen beroende på vilken direction som väljs så flyttas Ains position.
        /// Lägger även till en svans i en lista, precis som spelaren.
        /// </summary>
        /// <param name="food">Denna används för att få matens position.</param>
        /// <param name="wall">Denna används för att få väggarnas position.</param>
        public void FoodUpdate(GameObject food, List<Walls> wall)
        {
            AiFindingFood(food);

            int AiPrevY = Position.Y;
            int AiPrevX = Position.X;

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
            if (Tail.Count > 0)
            {
                Tail tailEnd = Tail[Tail.Count - 1];
                tailEnd.Position.X = AiPrevX;
                tailEnd.Position.Y = AiPrevY;
                Tail.Remove(tailEnd);
                Tail.Insert(0, tailEnd);
            }

            AiAvoidingWalls(wall);
        }

        public override void Update()
        {

        }
    }
}
