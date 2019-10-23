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
        private string viesti = "";

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
            Console.ReadKey();
            Console.Clear();
            TulostaAlkutarina();
            Kartta = new Kartta();
            Kartta.LueKartta();
            Pelaaja = Kartta.Pelaaja;
        }

        private void TulostaAlkutarina()
        {
            Console.WriteLine("Keilaniemi, Espoo");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Olet koodannut koko illan Academyn kampuksella.\nKahvikaan ei enää auta väsymykseen ja päätät lähteä kotiin Itä - Helsinkiin.\nEhdit illan viimeiseen metroon ja juna lähtee liikkeelle. \nLauttasaaren kohdalla silmäluomesi alkavat tuntua raskaalta. \nPilkit unen ja valveen rajamailla kunnes uni vie voiton...\nJatka painamalla mitä tahansa näppäintä");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Pimeä huone, Tuntematon sijainti");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Heräät pimeästä huoneesta. Et ole kotona.\nEt ole myöskään metrossa.\nKännykkäsi valolla löydät huoneesta uloskäynnin(X).\nMihin se mahtaa johtaa?");
            Console.ReadKey();
        }

        public void PeliSilmukka()
        {
            Kartta.TulostaPohja();
            while (true)
            {
                Console.WriteLine("liiku wasd-painikkeilla");
                Näppäin n = new Näppäin();
                char merkki = n.LueNäppäin();
                if (merkki == 'x') { break; }

                LiikutaPelaajaa(merkki);
                if(viesti != "")
                {
                    Console.WriteLine(viesti);
                    viesti = "";
                }

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
                Ylös();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Ylös();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus - 1, Pelaaja.Leveys] == '@' && !Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Ylös()
        {
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write(' ');
            Pelaaja.Ylös();
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write('O');
        }

        private void YritäLiikuttaaPelaajaaAlas()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys]))
            {
                Alas();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Alas();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus + 1, Pelaaja.Leveys] == '@' && !Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));
        }

        private void Alas()
        {
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write(' ');
            Pelaaja.Alas();
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write('O');
        }

        private void YritäLiikuttaaPelaajaaVasemmalle()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1]))
            {
                Vasen();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Vasen();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys - 1] == '@' && !Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Vasen()
        {
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write(' ');
            Pelaaja.Vasen();
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write('O');
        }

        private void YritäLiikuttaaPelaajaaOikealle()
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1]))
            {
                Oikea();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1] == '@' && Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Oikea();
            }
            else if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys + 1] == '@' && !Pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Oikea()
        {
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write(' ');
            Pelaaja.Oikea();
            Console.SetCursorPosition(Pelaaja.Leveys, Pelaaja.Korkeus);
            Console.Write('O');
        }

        private char GetKartanMerkki(int rivi, int sarake)
        {
            return Kartta.Pohja[rivi, sarake];
        }

        public void SeuraavaTaso()
        {
            Taso++;
            if (Taso == 2)
            {
                Kartta.Polku = "../../../Taso2.txt";
                Console.Clear();
                Console.WriteLine("Redi, Kalasatama");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Olet löytänyt itsesi Suomen suurimmasta tahattomasta pakohuoneesta. \nAinoa löytämäsi ovi(X) on lukittu(@). \nHuomaat tyhjissä liiketiloissa yksittäisiä avaimia(a, b, c, d, e, f). \nAvaisikohan jokin niistä lukon(@) vai oletko jumissa ikuisesti ?");
                Console.ReadKey();
                Console.Clear();
                Kartta.LueKartta();
                Kartta.TulostaPohja();
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
                Thread.Sleep(20);
            }
        }
    }
}
