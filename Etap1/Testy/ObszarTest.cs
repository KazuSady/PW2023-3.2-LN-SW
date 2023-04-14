using Dane;

namespace Testy
{
    [TestClass]
    public class ObszarTest
    {
        Obszar obszar = new Obszar(400, 400, 10, 10);
        [TestMethod]
        public void testGetter()
        {
            Assert.AreEqual(400, obszar.Height);
            Assert.AreEqual(400, obszar.Width);
            Assert.AreEqual(10, obszar.Kule.Count);
            Assert.AreEqual(10, obszar.Kule.ElementAt(0).R);
        }
        public void testSetter()
        {
            obszar.IsRunning = true;
            Assert.AreEqual(true, obszar.IsRunning);
        }
    }
}
