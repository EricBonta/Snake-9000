using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Ett interface för alla saker som ska renderas ut på skärmen som en symbol/char.
    /// Dessa ska också ha en färg.
    /// </summary>
    interface IRenderable
    {
        ConsoleColor RenderColor { get; set; }
        char RenderChar { get; set; }
    }
}
