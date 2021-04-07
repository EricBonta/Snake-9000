using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Classen för svansen som både spelaren och Ain får efter sig då den tar poäng.
    /// Den har en symbol, renderchar. Som säger vad svansen har för tecken, en position
    /// och en färg.
    /// </summary>
    public class Tail : GameObject, IRenderable
    {
        public char RenderChar { get; set; }

        public ConsoleColor RenderColor { get; set; }

        public Tail(char renderChar, int x, int y, ConsoleColor color)
            : base(x, y)
        {
            RenderChar = renderChar;
            RenderColor = color;
        }

        public override void Update()
        {

        }
    }
}
