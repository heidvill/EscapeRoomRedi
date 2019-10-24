using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;
using System.Threading;
using System.IO;

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

            TulostaAlkuruutu();
            Console.Clear();
            TulostaMerkkiKerrallaan("Tervetuloa pelaamaan! Mikä on nimesi?");
            //Console.WriteLine("Tervetuloa pelaamaan! Mikä on nimesi?");
            p.Nimi = Console.ReadLine();
            Console.WriteAscii($"Moi {p.Nimi}!", Color.FromArgb(DA, V, ID));
            Console.WriteLine("Aloita peli painamalla mitä tahansa näppäintä");
            Console.ReadKey();
            Console.Clear();
            TulostaAlkutarina();
            Kartta = new Kartta();
            Kartta.LueKartta();
            Kartta.TulostaPohja(Taso);
            Pelaaja = Kartta.Pelaaja;
        }

        private void TulostaAlkuruutu()
        {
            string[] lines = File.ReadAllLines(@"../../../alkuruutu.txt");
            
            foreach (string line in lines)
            {
                Console.WriteLine("\t" + line);
            }
            Console.ReadKey();

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
            Console.WriteLine("Liiku WASD-painikkeilla.");
            Console.ReadKey();
            Kartta.TulostaPohja(Taso);
            while (!GameOver)
            {
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
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] == 'Y')
                {
                    TulostaTripla();
                }
                if (Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] == 'W')
                {
                    BbLopetus();
                }
                else if ("abcdef".Contains(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]))
                {
                    Pelaaja.Ostoskärry.LisääAvain(Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys]);
                    Kartta.Pohja[Pelaaja.Korkeus, Pelaaja.Leveys] = ' ';
                }
            }
            Console.ReadKey();
        }

        public void BbLopetus()
        {
            Console.WriteLine($"Tämä on Big Brother. Tervetuloa taloon, {p.Nimi}.");
            Console.WriteLine("Hävisit pelin.");
            GameOver = true;
        }

        private void TulostaTripla()
        {
            Console.Clear();
            string[] lines = File.ReadAllLines(@"../../../tripla.txt");

            foreach (string line in lines)
            {
                Console.WriteLine("\t" + line);
            }
            GameOver = true;
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
                Console.WriteLine("Redin parkkihalli, Kalasatama");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Ovi aukesi ja löysit itsesi Redin parkkihallista. \nKuulet vaimeaa örinää. Onko se Saksaa? \nNäköpiiriisi osuu humalainen David Hasselhoff. \nHän haluaa laulaa sinulle serenadin. \nVälttele Hoffia ja etsi tie seuraavalle ovelle(X).");
                Console.ReadKey();
                Console.Clear();
                Kartta.LueKartta();
                Kartta.TulostaPohja(Taso);
            }
            else if (Taso == 4)
            {
                Kartta.Polku = "../../../Taso4.txt";
                Console.Clear();
                Console.WriteLine("Redin katto, Kalasatama");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Illan traumaattiset tapahtumat ovat tuoneet sinut valinnan äärelle. \nVoit ottaa riskialttiin pikahissin Itäväylälle tai jatkaa normihissillä tuntemattomaan.");
                Console.ReadKey();
                Console.Clear();
                Kartta.LueKartta();
                Kartta.TulostaPohja(Taso);

            }
            else if (Taso == 5)
            {
                Kartta.Polku = "../../../Taso5.txt";
                Console.Clear();
                Console.WriteLine("Redi, Valintojen maailma");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Olet selvinnyt pimeydestä, lukosta ja David Hasselhoffista.\nTodellinen haaste koittaa vasta nyt. \nNäet neljä ovea joista yksi vie sinut ulos Redistä. \n Kohtalosi voi olla erilainen jos valitset väärän oven.\n Tee valintasi(W,X,Y,Z).");
                Console.ReadKey();
                Console.Clear();
                Kartta.LueKartta();
                Kartta.TulostaPohja(Taso);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Onneksi olkoon, löysit tien takaisin ulkomaailmaan! \nMutta mitä ihmettä, kello on 7.45? Nyt kiireellä takaisin Keilaniemeen.");
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
