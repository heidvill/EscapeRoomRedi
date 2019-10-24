using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomRedi;

namespace EscapeRoomTestit
{
    [TestClass]
    public class PelaajaTesti
    {
        [TestMethod]
        public void PelaajaLiikkuuYlös()
        {
            Pelaaja p = new Pelaaja();
            p.Korkeus = 2;
            p.Ylös();
            Assert.AreEqual(1, p.Korkeus, "Pelaaja ei liikkunut ylöspäin");
        }

        [TestMethod]
        public void PelaajaLiikkuuAlas()
        {
            Pelaaja p = new Pelaaja();
            p.Korkeus = 2;
            p.Alas();
            Assert.AreEqual(3, p.Korkeus, "Pelaaja ei liikkunut alaspäin");
        }

        [TestMethod]
        public void PelaajaLiikkuuVasemmalle()
        {
            Pelaaja p = new Pelaaja();
            p.Leveys = 2;
            p.Vasen();
            Assert.AreEqual(1, p.Leveys, "Pelaaja ei liikkunut vasemmalle");
        }

        [TestMethod]
        public void PelaajaLiikkuuOikealle()
        {
            Pelaaja p = new Pelaaja();
            p.Leveys = 2;
            p.Oikea();
            Assert.AreEqual(3, p.Leveys, "Pelaaja ei liikkunut oikealle");
        }

        [TestMethod]
        public void PelaajanNimiEiOleTyhjä()
        {
            Pelaaja p = new Pelaaja();
            p.Nimi = "";
            Assert.AreEqual("Pelaaja", p.Nimi, "Pelaajan nimi ei ole oletus");
            Assert.AreEqual(7, p.Nimi.Length, "Pelaajan nimi ei ole oletusmittainen");
        }

        [TestMethod]
        public void PelaajanNimiAlkaaIsolla()
        {
            Pelaaja p = new Pelaaja();
            p.Nimi = "david";
            Assert.AreEqual("David", p.Nimi, "Pelaajan nimen ensimmäinen kirjain ei ole iso");
        }

        [TestMethod]
        public void PelaajanNimiYhdenMittainen()
        {
            Pelaaja p = new Pelaaja();
            p.Nimi = "d";
            Assert.AreEqual("D", p.Nimi, "Pelaajan nimi ei ala isolla alkukirjaimella");
        }
    }
}
