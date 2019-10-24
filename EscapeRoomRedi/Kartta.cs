
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;


namespace EscapeRoomRedi
{
    public class Kartta
    {
        public char[,] Pohja { get; set; }
        public Pelaaja Pelaaja { get; set; }
        public Pelaaja Hasselhoff { get; set; }

        public List<char> Esteet { get; set; }
        public string Polku { get; set; }
        //private string polku;
        public Kartta()
        {
            Polku = "../../../Taso1.txt";
            Pelaaja = new Pelaaja();
            Esteet = new List<char> { '#', '@' };
        }

        public void LueKartta()
        {
            StreamReader sr = new StreamReader(Polku);
            string rivi = "";
            int leveys = 0;
            int korkeus = 0;

            while (true)
            {
                rivi = sr.ReadLine();
                if (rivi == null)
                {
                    break;
                }
                leveys = rivi.Length;
                korkeus++;
            }
            Pohja = new char[korkeus, leveys];

            int i = 0; // rivi taulukossa
            int j = 0; // sarake taulukossa
            sr.DiscardBufferedData();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while (true)
            {
                rivi = sr.ReadLine();
                if (rivi == null)
                {
                    break;
                }
                foreach (char merkki in rivi)
                {
                    if (merkki == 'O')
                    {
                        Pelaaja.Korkeus = i;
                        Pelaaja.Leveys = j;
                        Pohja[i, j] = ' ';

                    }
                    else
                    {
                        Pohja[i, j] = merkki;
                    }
                    j++;

                }
                i++;
                j = 0;
            }
            sr.Dispose();
        }

        public void TulostaPohja(int taso)
        {
            Console.Clear();
            for (int i = 0; i < Pohja.GetLength(0); i++)
            {
                for (int j = 0; j < Pohja.GetLength(1); j++)
                {
                    if (i == Pelaaja.Korkeus && j == Pelaaja.Leveys)
                    {
                        Console.Write("O");
                    }
                    else if (taso == 3 && i ==Hasselhoff.Korkeus && j == Hasselhoff.Leveys)
                    {
                        Console.Write("H");
                    }
                    else
                    {
                        Color c = Color.White;
                        if (Pohja[i, j] == '#')
                        {
                            c = Color.Gray;
                        }
                        else if ("XYZW".Contains(Pohja[i, j]))
                        {
                            c = Color.Red;
                        }
                        else if (taso == 2 && "abcdef@".Contains(Pohja[i, j]))
                        {
                            c = Color.Yellow;
                        }

                        Console.Write(Pohja[i, j], c);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
