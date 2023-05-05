using Logika;

namespace Testy
{
    [TestClass]
    public class LogicAPITest
    {
        [TestMethod]
        public void logicAPITurnOnTurnOffTest()
        {
            AbstracDataAPI logicAPI = AbstracDataAPI.CreateAPI();
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
            AbstracDataAPI logicAPI = AbstracDataAPI.CreateAPI();
            logicAPI.CreateField(400, 400);
            logicAPI.CreateBalls(10, 10);
            Assert.IsTrue(10 == logicAPI.GetAllBalls().Count);
        }

        [TestMethod]
        public void logicAPIBallsMovingTest()
        {
            AbstracDataAPI logicAPI = AbstracDataAPI.CreateAPI();
            logicAPI.CreateField(400, 400);
            logicAPI.CreateBalls(10, 10);
            int prevX = logicAPI.GetAllBalls().First().Position.X;
            int prevY = logicAPI.GetAllBalls().First().Position.Y;

            logicAPI.TurnOn();
            Thread.Sleep(100);

            Assert.IsFalse(prevX == logicAPI.GetAllBalls().First().Position.X);
            Assert.IsFalse(prevY == logicAPI.GetAllBalls().First().Position.Y);

        }

    }
}
