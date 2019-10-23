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
    }
}
