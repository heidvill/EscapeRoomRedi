using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EscapeRoomRedi
{
    class Kartta
    {
        public char[,] Pohja { get; set; }
        public Pelaaja Pelaaja { get; set; }
        public void Luekartta()
        {
            string polku = "../../../Taso1.txt";
            StreamReader sr = new StreamReader(polku);
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
                if(rivi == null)
                {
                    break;
                }
                foreach (char merkki in rivi)
                {
                    if (merkki == 'O')
                    { Pelaaja = new Pelaaja(i, j); }
                    Pohja[i, j] = merkki;
                    j++;
                }
                i++;
                j = 0;
            }

            TulostaPohja();
        }

        public void TulostaPohja()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(Pohja[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
}
