using Dane;
using System.Drawing;

namespace Testy
{
    [TestClass]
    public class DataAPITest
    {

        [TestMethod]
        public void dataAPIBallPOsitionTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            dataAPI.CreateBall(new Point(10, 10));
            Assert.IsTrue(dataAPI.GetAllBalls().First().Position.X == 10);
            Assert.IsTrue(dataAPI.GetAllBalls().First().Position.Y == 10);
        }

        [TestMethod]
        public void DataAPIBallMovementTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            dataAPI.CreateBall(new Point(10, 10));
            dataAPI.GetAllBalls().First().YMovement = 1;
            dataAPI.GetAllBalls().First().XMovement = 1;

            Assert.IsTrue(dataAPI.GetAllBalls().First().YMovement == 1);
            Assert.IsTrue(dataAPI.GetAllBalls().First().XMovement == 1);
        }

        [TestMethod]
        public void dataAPIBallsMovingTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            dataAPI.CreateBall(new Point(10, 10));
            IBall ball = dataAPI.GetAllBalls().First();

            ball.YMovement = 10;
            ball.XMovement = 10;
            Assert.IsTrue(ball.YMovement == 10);
            Assert.IsTrue(ball.XMovement == 10);

            int prevX = ball.Position.X;
            int prevY = ball.Position.Y;

            Thread.Sleep(20);

            Assert.AreNotEqual(prevX, ball.Position.X);
            Assert.AreNotEqual(prevY, ball.Position.Y);

        }

        [TestMethod]
        public void dataAPITurnOnTurnOffTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            dataAPI.CreateScene(400, 400);
            Assert.AreEqual(false, dataAPI.IsRunning());

            dataAPI.TurnOn();
            Assert.AreEqual(true, dataAPI.IsRunning());

            dataAPI.TurnOff();
            Assert.AreEqual(false, dataAPI.IsRunning());
        }

        [TestMethod]
        public void dataAPICreateBallsTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            dataAPI.CreateScene(400, 400);
            for (int i = 0; i < 10; i++)
            {
                Point position = new Point(10, 10);
                dataAPI.CreateBall(position);
            }
            Assert.IsTrue(10 == dataAPI.GetAllBalls().Count);
        }

    }
}
