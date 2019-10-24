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
        Pelaaja hasselhoff;
        private string viesti = "";

        public void AloitaPeli()
        {
            int DA = 244;
            int V = 212;
            int ID = 255;

            TulostaAlkuruutu();
            Console.Clear();
            TulostaMerkkiKerrallaan("Tervetuloa pelaamaan! Mikä on nimesi?");
            System.Console.WriteLine();
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
            Console.WriteLine("liiku wasd-painikkeilla");
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
                if (Taso == 3)
                {
                    LiikutaHasselhoffiaKohteeseen();
                    if (TheHoffSaaPelaajanKiinni())
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
                        LuoHasselhoff();
                        Kartta.TulostaPohja(Taso);
                    }
                }
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
                    YritäLiikuttaaPelaajaaYlös(Pelaaja);
                    break;
                case 's':
                    YritäLiikuttaaPelaajaaAlas(Pelaaja);
                    break;
                case 'a':
                    YritäLiikuttaaPelaajaaVasemmalle(Pelaaja);
                    break;
                case 'd':
                    YritäLiikuttaaPelaajaaOikealle(Pelaaja);
                    break;
                default:
                    break;
            }
        }

        private void YritäLiikuttaaPelaajaaYlös(Pelaaja pelaaja)
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[pelaaja.Korkeus - 1, pelaaja.Leveys]))
            {
                Ylös(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus - 1, pelaaja.Leveys] == '@' && pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Ylös(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus - 1, pelaaja.Leveys] == '@' && !pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Ylös(Pelaaja pelaaja)
        {
            char hahmo = 'O';
            if (pelaaja.Nimi == "H")
            {
                hahmo = 'H';
            }
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(' ');
            pelaaja.Ylös();
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(hahmo);
        }

        private void YritäLiikuttaaPelaajaaAlas(Pelaaja pelaaja)
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[pelaaja.Korkeus + 1, pelaaja.Leveys]))
            {
                Alas(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus + 1, pelaaja.Leveys] == '@' && pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Alas(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus + 1, pelaaja.Leveys] == '@' && !pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));
        }

        private void Alas(Pelaaja pelaaja)
        {
            char hahmo = 'O';
            if (pelaaja.Nimi == "H")
            {
                hahmo = 'H';
            }
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(' ');
            pelaaja.Alas();
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(hahmo);
        }

        private void YritäLiikuttaaPelaajaaVasemmalle(Pelaaja pelaaja)
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys - 1]))
            {
                Vasen(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys - 1] == '@' && pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Vasen(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys - 1] == '@' && !pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Vasen(Pelaaja pelaaja)
        {
            char hahmo = 'O';
            if (pelaaja.Nimi == "H")
            {
                hahmo = 'H';
            }
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(' ');
            pelaaja.Vasen();
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(hahmo);
        }

        private void YritäLiikuttaaPelaajaaOikealle(Pelaaja pelaaja)
        {
            if (!Kartta.Esteet.Contains(Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys + 1]))
            {
                Oikea(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys + 1] == '@' && pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                Oikea(pelaaja);
            }
            else if (Kartta.Pohja[pelaaja.Korkeus, pelaaja.Leveys + 1] == '@' && !pelaaja.Ostoskärry.Avaimet.Contains('e'))
            {
                viesti = "Sinulla ei ole oikeaa avainta";
            }
            Console.SetCursorPosition(0, Kartta.Pohja.GetLength(0));

        }

        private void Oikea(Pelaaja pelaaja)
        {
            char hahmo = 'O';
            if(pelaaja.Nimi == "H")
            {
                hahmo = 'H';
            }
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(' ');
            pelaaja.Oikea();
            Console.SetCursorPosition(pelaaja.Leveys, pelaaja.Korkeus);
            Console.Write(hahmo);
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
                LuoHasselhoff();
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
            Console.Clear();
            Console.WriteLine("Voi rähmä. Kompastuit ja putosit kattoikkunan läpi takaisin pimeään huoneeseen josta aloitit..");
            Console.ReadKey();
            Taso = 1;
            Kartta.Polku = "../../../Taso1.txt";
            Kartta.LueKartta();
            Kartta.TulostaPohja(Taso);
   
        }


        private void LuoHasselhoff()
        {
            Random r = new Random();
            hasselhoff = new Pelaaja();
            hasselhoff.Nimi = "H";
            hasselhoff.Korkeus = r.Next(0, Kartta.Pohja.GetLength(0));
            hasselhoff.Leveys = r.Next(0, Kartta.Pohja.GetLength(1));
            char merkki = Kartta.Pohja[hasselhoff.Korkeus, hasselhoff.Leveys];
            while (merkki=='#' || merkki == 'O')
            {
                hasselhoff.Korkeus = r.Next(0, Kartta.Pohja.GetLength(0));
                hasselhoff.Leveys = r.Next(0, Kartta.Pohja.GetLength(1));
                merkki = Kartta.Pohja[hasselhoff.Korkeus, hasselhoff.Leveys];
            }
            Kartta.Hasselhoff = hasselhoff;

        }

        private void LiikutaHasselhoffiaSatunnaisesti()
        {
            Random r = new Random();
            int suunta = r.Next(0, 4);
            if(suunta == 0) // ylös
            {
                YritäLiikuttaaPelaajaaYlös(hasselhoff);
            }
            else if (suunta == 1) // alas
            {
                YritäLiikuttaaPelaajaaAlas(hasselhoff);
            }
            else if (suunta == 2) // vasen
            {
                YritäLiikuttaaPelaajaaVasemmalle(hasselhoff);
            }
            else // oikea
            {
                YritäLiikuttaaPelaajaaOikealle(hasselhoff);
            }
        }

        private void LiikutaHasselhoffiaKohteeseen()
        {
            Random r = new Random();

            if ((r.Next(0, 11) < 5 && hasselhoff.Korkeus != Pelaaja.Korkeus) || hasselhoff.Leveys == Pelaaja.Leveys)
            {
                if (Pelaaja.Korkeus < hasselhoff.Korkeus) { YritäLiikuttaaPelaajaaYlös(hasselhoff); }
                else if (Pelaaja.Korkeus > hasselhoff.Korkeus) { YritäLiikuttaaPelaajaaAlas(hasselhoff); }
            }
            else
                if (Pelaaja.Leveys < hasselhoff.Leveys) { YritäLiikuttaaPelaajaaVasemmalle(hasselhoff); }
                else if (Pelaaja.Leveys > hasselhoff.Leveys) { YritäLiikuttaaPelaajaaOikealle(hasselhoff); }
        }

        private bool TheHoffSaaPelaajanKiinni()
        {
            if (Pelaaja.Korkeus == hasselhoff.Korkeus && Pelaaja.Leveys == hasselhoff.Leveys) return true;
            return false;
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
