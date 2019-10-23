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
        public int Taso { get; set; } = 1;

        Pelaaja p = new Pelaaja();
        public void AloitaPeli()
        {
            Console.WriteLine("Tervetuloa pelaamaan! Mikä on nimesi?");
            p.Nimi = Console.ReadLine();
            Console.Write($"Moi {p.Nimi}!\nAloita peli painamalla mitä tahansa näppäintä");
            Console.ReadLine();
            Console.WriteLine("Pelin aloitustarina\nJatka painamalla mitä tahansa näppäintä");
            Console.ReadLine();
            Kartta = new Kartta();
            Kartta.LueKartta();
            Pelaaja = Kartta.Pelaaja;            
        }
      
        public void PeliSilmukka()
        {
            while (true)
            {
                Console.WriteLine("liiku wasd-painikkeilla");
                Näppäin n = new Näppäin();
                char merkki = n.LueNäppäin();
                if (merkki == 'x') { break; }
                LiikutaPelaajaa(merkki);
                Kartta.TulostaPohja();
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] == 'X')
                {                    
                    SeuraavaTaso();
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
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys]))
                    {
                        Pelaaja.Ylös();
                    }
                    else if (Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
                    {
                        Pelaaja.Ylös();
                    }
                    break;
                case 's':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys]))
                    {
                        Pelaaja.Alas();
                    }
                    else if (Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
                    {
                        Pelaaja.Alas();
                    }
                    break;
                case 'a':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1]))
                    {
                        Pelaaja.Vasen();
                    }
                    else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys -1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
                    {
                        Pelaaja.Vasen();
                    }
                    break;
                case 'd':
                    if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1]))
                    {
                        Pelaaja.Oikea();
                    }
                    else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
                    {
                        Pelaaja.Oikea();
                    }
                    break;
                default:
                    break;
            }


        }
        public void SeuraavaTaso()
        {
            Taso++;
            if (Taso == 2)
            {

                Kartta.Polku = "../../../Taso2.txt";
                Kartta.LueKartta();
            }
        }
    }
}
