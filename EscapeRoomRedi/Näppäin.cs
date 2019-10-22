using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    class Näppäin
    {
        public char input { get; set; }
        ConsoleKeyInfo näppäin;

        public void LueNäppäin()
        {
            näppäin = Console.ReadKey();
            input = näppäin.KeyChar;
            Console.WriteLine();
            Console.WriteLine(input);

        }

    }
}
