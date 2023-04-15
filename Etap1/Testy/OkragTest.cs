using Model;
using Logika;


namespace Testy
{
    [TestClass]
    public class OkragTest
    {
        [TestMethod]
        public void okragGetter()
        {
            Kula kula = new Kula(10, 10, 10);
            Okrag okrag = new Okrag(kula);

            Assert.AreEqual(okrag.X, kula.X);
            Assert.AreEqual(okrag.Y, kula.Y);
            Assert.AreEqual(okrag.R, kula.R);
        }
        [TestMethod]
        public void okragSetter()
        {
            Kula kula = new Kula(10, 10, 10);
            Okrag okrag = new Okrag(kula);
            okrag.X = 5;
            okrag.Y = 5;
            okrag.R = 5;

            Assert.AreEqual(okrag.X, 5);
            Assert.AreEqual(okrag.Y, 5);
            Assert.AreEqual(okrag.R, 5);
        }
    }
}
