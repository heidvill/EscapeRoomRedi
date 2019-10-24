using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    public class Pelaaja
    {
        public int Korkeus { get; set; }
        public int Leveys { get; set; }
        private string nimi;
        public string Nimi
        {
            get { return nimi; }
            set
            {
                value = value.ToLower();
                if (value.Length < 1)
                {
                    nimi = "Pelaaja";
                }
                else
                {
                    string temp = (value[0] + "").ToUpper();
                    if (value.Length > 1)
                    {
                        temp += value.Substring(1, value.Length - 1);
                    }
                    nimi = temp;
                }
            }
        }

        public Ostoskärry Ostoskärry { get; set; } = new Ostoskärry();


        public Pelaaja(int korkeus, int leveys, string nimi)
        {
            Korkeus = korkeus;
            Leveys = leveys;
            Nimi = nimi;
        }

        public Pelaaja()
        {
        }

        public void Ylös()
        {
            Korkeus--;
        }

        public void Alas()
        {
            Korkeus++;
        }

        public void Vasen()
        {
            Leveys--;
        }

        public void Oikea()
        {
            Leveys++;
        }
    }


}
