using Model;

namespace Testy
{
    [TestClass]
    public class ModelAPITest
    {
        [TestMethod]
        public void logicAPIGettersTest()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();

            modelAPI.TurnOn(400, 400, 10, 10);
            Assert.AreEqual(true, modelAPI.IsRunning());

            modelAPI.TurnOff();
            Assert.AreEqual(false, modelAPI.IsRunning());
        }

    }
}
