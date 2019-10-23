using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomRedi;

namespace EscapeRoomTestit
{
    [TestClass]
    public class PelaajaTesti
    {
        [TestMethod]
        public void PelaajaLiikkuuYl�s()
        {
            Pelaaja p = new Pelaaja();
            p.Korkeus = 2;
            p.Yl�s();
            Assert.AreEqual(1, p.Korkeus, "Pelaaja ei liikkunut yl�sp�in");
        }

        [TestMethod]
        public void PelaajaLiikkuuAlas()
        {
            Pelaaja p = new Pelaaja();
            p.Korkeus = 2;
            p.Alas();
            Assert.AreEqual(3, p.Korkeus, "Pelaaja ei liikkunut alasp�in");
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
