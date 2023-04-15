using Logika;

namespace Testy
{
    [TestClass]
    public class KulaTest
    {
        Kula kula = new Kula(10,10,10);

        [TestMethod]
        public void testGetter()
        {
            Assert.AreEqual(10, kula.X);
            Assert.AreEqual(10, kula.Y);
            Assert.AreEqual(10, kula.R);
        }
        [TestMethod]
        public void testSetter()
        {
            kula.X = 20;
            kula.Y = 20;
            kula.R = 20;

            Assert.AreEqual(20, kula.X);
            Assert.AreEqual (20, kula.Y);
            Assert.AreEqual(20, kula.R);
        }
    }
}