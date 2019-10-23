using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    public class Näppäin
    {
        public char input { get; set; }
        ConsoleKeyInfo näppäin;

        public char LueNäppäin()
        {
            näppäin = Console.ReadKey();
            input = näppäin.KeyChar;
            return input;
        }

    }
}
