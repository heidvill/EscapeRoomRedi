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
            Kartta.LueKartta();
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
                        if (Kartta.Pohja[Pelaaja.Korkeus-1, Pelaaja.Leveys]!='#')
                        {
                        Pelaaja.Ylös();
                        }
                        break;
                    case 's':
                        if (Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys] != '#')
                        {
                            Pelaaja.Alas();
                        }
                        break;
                    case 'a':
                        if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys -1] != '#')
                        {
                            Pelaaja.Vasen();
                        }
                        break;
                    case 'd':
                        if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys +1] != '#')
                        {
                            Pelaaja.Oikea();
                        }
                        break;
                    default:
                        break;
                }
                Kartta.TulostaPohja();
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]== 'X')
                {
                    Kartta.SeuraavaTaso();
                }
            }

        }
    }
}
