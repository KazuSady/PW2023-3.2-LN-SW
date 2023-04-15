using Logika;

namespace Testy
{
    [TestClass]
    public class ObszarTest
    {
        Obszar obszar = new Obszar(400, 400);

        [TestMethod]
        public void testGetter()
        {
            obszar.CreateKulaList(10, 10);
            Assert.AreEqual(400, obszar.Height);
            Assert.AreEqual(400, obszar.Width);
            Assert.AreEqual(10, obszar.Kule.Count);
            Assert.AreEqual(10, obszar.Kule.ElementAt(0).R);
            Assert.AreEqual(false, obszar.IsRunning);
        }

        [TestMethod]
        public void testSetter()
        {
            obszar.IsRunning = true;
            Assert.AreEqual(true, obszar.IsRunning);
        }
    }
}
