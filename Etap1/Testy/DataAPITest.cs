using Dane;
using Logika;

namespace Testy
{
    [TestClass]
    public class DataAPITest
    {
        [TestMethod]
        public void dataAPITurnOnTurnOffTest()
        {
            AbstracDataAPI dataAPI = AbstracDataAPI.CreateAPI();
            dataAPI.CreateField(400, 400);
            Assert.AreEqual(false, dataAPI.IsRunning());

            dataAPI.TurnOn();
            Assert.AreEqual(true, dataAPI.IsRunning());

            dataAPI.TurnOff();
          Assert.AreEqual(false, dataAPI.IsRunning());
        }

        [TestMethod]
        public void dataAPICreateBallsTest()
        {
            AbstracDataAPI dataAPI = AbstracDataAPI.CreateAPI();
            dataAPI.CreateField(400, 400);
            dataAPI.CreateBalls(10, 10);
            Assert.IsTrue(10 == dataAPI.GetAllBalls().Count);
        }

        [TestMethod]
        public void dataAPIBallsMovingTest()
        {
            AbstracDataAPI dataAPI = AbstracDataAPI.CreateAPI();
            dataAPI.CreateField(400, 400);
            dataAPI.CreateBalls(10, 10);
            int prevX = dataAPI.GetAllBalls().First().Position.X;
            int prevY = dataAPI.GetAllBalls().First().Position.Y;

            dataAPI.TurnOn();
            Thread.Sleep(100);

            Assert.IsFalse(prevX == dataAPI.GetAllBalls().First().Position.X);
            Assert.IsFalse(prevY == dataAPI.GetAllBalls().First().Position.Y);

        }
    }
}
