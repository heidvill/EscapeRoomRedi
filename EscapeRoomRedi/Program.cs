using System;

namespace EscapeRoomRedi
{
    class Program
    {
        static void Main(string[] args)
        {
            Kartta k = new Kartta();
            k.Luekartta();

            Console.WriteLine("paina jotain");
            Näppäin n = new Näppäin();
            n.LueNäppäin();
        }
    }
}
