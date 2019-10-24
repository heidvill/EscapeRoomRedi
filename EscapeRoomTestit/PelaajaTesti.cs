using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomRedi;

namespace EscapeRoomTestit
{
    [TestClass]
    public class Ostosk‰rryTesti
    {
        [TestMethod]
        public void Ostosk‰rryss‰OnLista()
        {
            Ostosk‰rry o = new Ostosk‰rry();
            Assert.IsNotNull(o.Avaimet);
        }

        [TestMethod]
        public void AvaimenLis‰ysToimii()
        {
            Ostosk‰rry o = new Ostosk‰rry();
            o.Lis‰‰Avain('a');
            Assert.AreEqual(1, o.Avaimet.Count, "Avaimia on v‰‰r‰ m‰‰r‰");
        }
    }
}
