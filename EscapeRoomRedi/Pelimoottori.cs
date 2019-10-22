using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    class Pelimoottori
    {
        public Kartta Kartta { get; set; }
        public Näppäin Näppäin { get; set; }
        public Pelaaja Pelaaja { get; set; }

        public void AloitaPeli()
        {
            Kartta = new Kartta();
            Kartta.Luekartta();
            Pelaaja = Kartta.Pelaaja;

        }
        public void PeliSilmukka()
        {
            while (true)
            {
            Console.WriteLine("paina jotain");
            Näppäin n = new Näppäin();
            char merkki = n.LueNäppäin();
            if (merkki == 'x') { break; }
                switch (merkki)
                {
                    case 'w': 
                        Pelaaja.Ylös();
                        break;
                    case 's':
                        Pelaaja.Alas();
                        break;
                    case 'a':
                        Pelaaja.Vasen();
                        break;
                    case 'd':
                        Pelaaja.Oikea();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
