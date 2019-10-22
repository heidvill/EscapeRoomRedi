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
        public int Taso { get; set; } = 1;
        private string polku;
        public Kartta()
        {
            polku = "../../../Taso1.txt";
            Pelaaja = new Pelaaja();
        }

        public void LueKartta()
        {
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
            TulostaPohja();
        }

        public void TulostaPohja()
        {
            Console.Clear();
            for (int i = 0; i < Pohja.GetLength(0); i++)
            {
                for (int j = 0; j < Pohja.GetLength(1); j++)
                {
                    if (i==Pelaaja.Korkeus && j==Pelaaja.Leveys)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                    Console.Write(Pohja[i, j]);

                    }
                }
                Console.WriteLine();
            }
            
        }

        public void SeuraavaTaso()
        {
            Taso++;
            if (Taso == 2)
            {
                polku = "../../../Taso2.txt";
                LueKartta();

            }
        }

    }
}
