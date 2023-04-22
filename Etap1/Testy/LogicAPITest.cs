using Logika;

namespace Testy
{
    [TestClass]
    public class LogicAPITest
    {
        [TestMethod]
        public void logicAPITurnOnTurnOffTest()
        {
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateAPI();
            logicAPI.CreateField(400, 400);
            Assert.AreEqual(false, logicAPI.IsRunning());

            logicAPI.TurnOn();
            Assert.AreEqual(true, logicAPI.IsRunning());

            logicAPI.TurnOff();
            Assert.AreEqual(false, logicAPI.IsRunning());
        }
        [TestMethod]
        public void logicAPICreateBallsTest()
        {
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateAPI();
            logicAPI.CreateField(400, 400);
            logicAPI.CreateBalls(10, 10);
            Assert.AreEqual(false, logicAPI.IsRunning());

            logicAPI.TurnOn();
            Assert.AreEqual(true, logicAPI.IsRunning());

            logicAPI.TurnOff();
            Assert.AreEqual(false, logicAPI.IsRunning());
        }

    }
}
