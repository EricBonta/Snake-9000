using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000
{
    /// <summary>
    /// Ett interface för att object som ska kunna röra på sig i en riktning.
    /// </summary>
    public interface IMoveable
    {
        public Direction Dir { get; set; }
    }
}
