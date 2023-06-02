using Dane;
using System.Drawing;
using System.Numerics;

namespace Testy
{
    [TestClass]
    public class DataAPITest
    {

        [TestMethod]
        public void dataAPIBallPOsitionTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            AbstractBallLogger abstractBallLogger = AbstractBallLogger.CreateBallLoger();
            dataAPI.CreateBall(1, 10, 10, abstractBallLogger);
            Assert.IsTrue(dataAPI.GetAllBalls().First().Position.X == 10);
            Assert.IsTrue(dataAPI.GetAllBalls().First().Position.Y == 10);
        }

        [TestMethod]
        public void DataAPIBallMovementTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            AbstractBallLogger abstractBallLogger = AbstractBallLogger.CreateBallLoger();
            dataAPI.CreateBall(1, 10, 10, abstractBallLogger);
            dataAPI.GetAllBalls().First().Movement = new Vector2(1, 1);


            Assert.IsTrue(dataAPI.GetAllBalls().First().Movement.X == 1);
            Assert.IsTrue(dataAPI.GetAllBalls().First().Movement.Y == 1);
        }

        [TestMethod]
        public void dataAPIBallsMovingTest()
        {
            AbstractDataAPI dataAPI = AbstractDataAPI.CreateApi();
            AbstractBallLogger abstractBallLogger = AbstractBallLogger.CreateBallLoger();
            dataAPI.CreateBall(1, 10, 10, abstractBallLogger);
            IBall ball = dataAPI.GetAllBalls().First();

            Assert.IsTrue(ball.Position.X == 10);
            Assert.IsTrue(ball.Position.Y == 10);
            Vector2 prevMovement = ball.Position;

            ball.Movement = new Vector2(10, 10);

            dataAPI.TurnOn();
            Thread.Sleep(20);

            Assert.AreNotEqual(prevMovement.X, ball.Position.X);
            Assert.AreNotEqual(prevMovement.Y, ball.Position.Y);

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
            AbstractBallLogger abstractBallLogger = AbstractBallLogger.CreateBallLoger();
            dataAPI.CreateScene(400, 400);
            for (int i = 0; i < 10; i++)
            {
                dataAPI.CreateBall(i, 10 * i, 10 * i, abstractBallLogger);
            }
            Assert.IsTrue(10 == dataAPI.GetAllBalls().Count);
        }

    }
}
