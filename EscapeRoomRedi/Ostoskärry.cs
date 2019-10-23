using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeRoomRedi
{
    public class Ostoskärry
    {
        public List<char> Avaimet  { get; set; }

        public Ostoskärry()
        {
            Avaimet = new List<char>();
        }

        public void LisääAvain(char avain)
        {
            Avaimet.Add(avain);
        }

    }
}
