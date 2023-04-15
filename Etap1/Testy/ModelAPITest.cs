using Model;

namespace Testy
{
    [TestClass]
    public class ModelAPITest
    {
        [TestMethod]
        public void modelAPITests()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();
            modelAPI.CreateObszar(400, 400, 10, 10);
            Assert.AreEqual(modelAPI.GetOkragList().Count, 10);
            Assert.AreEqual(modelAPI.GetOkragList().ElementAt(0).R, 10);
            Assert.AreEqual(modelAPI.isRunning(), false);

            modelAPI.turnOn();
            Assert.AreEqual(modelAPI.isRunning(), true);

            modelAPI.turnOff();
            Assert.AreEqual(modelAPI.isRunning(), false);
        }
    }
}
