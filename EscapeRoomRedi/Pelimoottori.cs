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
                LiikutaPelaajaa(merkki);
                Kartta.TulostaPohja();
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] == 'X')
                {
                    Kartta.SeuraavaTaso();
                }
                else if ("abcdef".Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]))
                {
                    Pelaaja.Ostoskärry.LisääAvain(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]);
                    Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] = ' ';
                }
            }

        }

        private void LiikutaPelaajaa(char näppäin)
        {
            
            
                switch (näppäin)
                {
                    case 'w':
                        if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys]) )
                        {
                            Pelaaja.Ylös();
                        }
                        break;
                case 's':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys]))
                    {
                        Pelaaja.Alas();
                    }
                    break;
                case 'a':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1]))
                    {
                        Pelaaja.Vasen();
                    }
                    break;
                case 'd':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1]))
                    {
                        Pelaaja.Oikea();
                    }
                    break;
                default:
                    break;
            }
            
            
        }
    }
}
