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
            Assert.AreEqual(modelAPI.IsRunning(), false);

            modelAPI.TurnOn();
            Assert.AreEqual(modelAPI.IsRunning(), true);

            modelAPI.TurnOff();
            Assert.AreEqual(modelAPI.IsRunning(), false);
        }
    }
}
