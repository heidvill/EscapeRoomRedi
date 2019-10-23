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
        public bool GameOver { get; set; } = false;

        Pelaaja p = new Pelaaja();
        private string viesti = "";

        public void AloitaPeli()
        {
            int DA = 244;
            int V = 212;
            int ID = 255;

            TulostaMerkkiKerrallaan("Tervetuloa pelaamaan! Mikä on nimesi?");
            //Console.WriteLine("Tervetuloa pelaamaan! Mikä on nimesi?");
            p.Nimi = Console.ReadLine();
            try
            {
                Console.WriteAscii($"Moi {p.Nimi}!", Color.FromArgb(DA, V, ID));
            }
            catch
            {
                p.Nimi = "Pelaaja";
                Console.WriteAscii($"Moi {p.Nimi}!", Color.FromArgb(DA, V, ID));
            }
            Console.WriteLine("Aloita peli painamalla mitä tahansa näppäintä");
            Console.ReadKey();
            Console.Clear();
            TulostaAlkutarina();
            Kartta = new Kartta();
            Kartta.LueKartta();
            Kartta.TulostaPohja(Taso);
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
            Kartta.TulostaPohja(Taso);
            while (!GameOver)
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
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] == 'Z')
                {
                    PalaaAlkuun();
                }
                else if ("abcdef".Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]))
                {
                    Pelaaja.Ostoskärry.LisääAvain(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]);
                    Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] = ' ';
                }
            }
            Console.ReadKey();
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
                Kartta.TulostaPohja(Taso);
            }
            else if (Taso == 3)
            {
                Kartta.Polku = "../../../Taso3.txt";
                Console.Clear();
                Console.WriteLine("Redin katto, Kalasatama");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Illan traumaattiset tapahtumat ovat tuoneet sinut valinnan äärelle. Voit ottaa riskialttiin pikahissin Itäväylälle tai jatkaa normihissillä tuntemattomaan.");
                Console.ReadKey();
                Console.Clear();
                Kartta.LueKartta();
                Kartta.TulostaPohja(Taso);

            }
            else
            {
                Console.WriteLine("Onneksi olkoon, löysit tien ulkoilmaan. Mutta mitä ihmettä, kello on 7.45? Nyt kiireellä takaisin Keilaniemeen.");
                GameOver = true;
            }
        }
        public void PalaaAlkuun()
        {

            Console.WriteLine("Voi rähmä. Kompastuit ja putosit kattoikkunan läpi takaisin pimeään huoneeseen josta aloitit..");
            Taso = 1;
            Kartta.Polku = "../../../Taso1.txt";
            Kartta.LueKartta();
            Kartta.TulostaPohja(Taso);
   
        }


        public void TulostaMerkkiKerrallaan(string tulostettava)
        {
            foreach (char merkki in tulostettava)
            {
                Console.Write(merkki);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }
    }
}
