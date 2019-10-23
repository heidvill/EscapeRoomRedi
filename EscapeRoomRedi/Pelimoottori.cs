using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;
using System.Threading;

namespace EscapeRoomRedi
{
    public class Pelimoottori
    {
        public Kartta Kartta { get; set; }
        public Näppäin Näppäin { get; set; }
        public Pelaaja Pelaaja { get; set; }
        public int Taso { get; set; } = 1;

        Pelaaja p = new Pelaaja();
        public void AloitaPeli()
        {
            int DA = 244;
            int V = 212;
            int ID = 255;

            TulostaMerkkiKerrallaan("Tervetuloa pelaamaan! Mikä on nimesi?");
            Console.WriteLine("Tervetuloa pelaamaan! Mikä on nimesi?");
            p.Nimi = Console.ReadLine();
            Console.WriteAscii($"Moi {p.Nimi}!", Color.FromArgb(DA, V, ID));
            Console.WriteLine("Aloita peli painamalla mitä tahansa näppäintä");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Keilaniemi, Espoo");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Olet koodannut koko illan Academyn kampuksella.\nKahvikaan ei enää auta väsymykseen ja päätät lähteä kotiin Itä - Helsinkiin.\nEhdit illan viimeiseen metroon ja juna lähtee liikkeelle. \nLauttasaaren kohdalla silmäluomesi alkavat tuntua raskaalta. \nPilkit unen ja valveen rajamailla kunnes uni vie voiton...\nJatka painamalla mitä tahansa näppäintä");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Pimeä huone, Tuntematon sijainti");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Heräät pimeästä huoneesta. Et ole kotona.\nEt ole myöskään metrossa.\nKännykkäsi valolla löydät huoneesta uloskäynnin(X).\nMihin se mahtaa johtaa?");
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
                    YritäLiikuttaaPelaajaaYlös();
                    break;
                case 's':
                    YritäLiikuttaaPelaajaaAlas();
                    break;
                case 'a':
                    YritäLiikuttaaPelaajaaVasemmalle();
                    break;
                case 'd':
                    YritäLiikuttaaPelaajaaOikealle();
                    break;
                default:
                    break;
            }
        }

        private void YritäLiikuttaaPelaajaaYlös()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys]))
            {
                Pelaaja.Ylös();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Pelaaja.Ylös();
            }
        }

        private void YritäLiikuttaaPelaajaaAlas()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys]))
            {
                Pelaaja.Alas();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Pelaaja.Alas();
            }
        }

        private void YritäLiikuttaaPelaajaaVasemmalle()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1]))
            {
                Pelaaja.Vasen();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Pelaaja.Vasen();
            }
        }

        private void YritäLiikuttaaPelaajaaOikealle()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1]))
            {
                Pelaaja.Oikea();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Pelaaja.Oikea();
            }
        }

        public void SeuraavaTaso()
        {
            Taso++;
            if (Taso == 2)
            {
                Kartta.Polku = "../../../Taso2.txt";
                Console.Clear();
                Console.WriteLine("Redi, Kalasatama");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Olet löytänyt itsesi Suomen suurimmasta tahattomasta pakohuoneesta. \nAinoa löytämäsi ovi on lukossa. \nHuomaat tyhjissä liiketiloissa yksittäisiä avaimia(a, b, c, d, e, f). \nAvaisikohan jokin niistä oven vai oletko jumissa ikuisesti ?");
                Console.ReadLine();
                Console.Clear();
                Kartta.LueKartta();
            }
            else
            {
                Console.WriteLine("Onneksi olkoon, löysit tien ulkoilmaan. Mutta mitä ihmettä, kello on 7.45? Nyt kiireellä takaisin Keilaniemeen.");
            }
        }

        public void TulostaMerkkiKerrallaan(string tulostettava)
        {
            foreach (char merkki in tulostettava)
            {
                Console.Write(merkki);
                Thread.Sleep(50);
            }
        }
    }
}
