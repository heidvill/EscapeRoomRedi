using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    public class Pelaaja
    {
        public int Korkeus { get; set; }
        public int Leveys { get; set; }
        public string Nimi { get; set; }

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
