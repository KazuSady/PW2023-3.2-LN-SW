using Dane;
using Logika;
using System.Drawing;
using System.Numerics;

namespace Testy
{
    [TestClass]
    public class LogicAPITest
    {
        internal class FakeDataAPI : AbstractDataAPI
        {
            int ballRadius = 10;
            private int sceneHeight;
            private int sceneWidth;
            private bool isRunning;
            List<IBall> _ballList = new List<IBall>();

            public override void CreateBall(int id, int x, int y, AbstractBallLogger logger)
            {
                Random random = new Random();
                x = random.Next(ballRadius, this.GetSceneWidth() - ballRadius);
                y = random.Next(ballRadius, this.GetSceneHeight() - ballRadius);

                _ballList.Add(IBall.CreateBall(1, x, y, logger));
                do
                {
                    _ballList.Last().Movement = new Vector2(random.Next(-10000, 10000) % 3, random.Next(-10000, 10000) % 3);
                } while (_ballList.Last().Movement.X == 0 || _ballList.Last().Movement.Y == 0);

            }

            public override void CreateScene(int height, int width)
            {
                sceneHeight = height;
                sceneWidth = width;
            }

            public override List<IBall> GetAllBalls()
            {
                return _ballList;
            }

            public override int GetSceneHeight()
            {
                return sceneHeight;
            }

            public override int GetSceneWidth()
            {
                return sceneWidth;
            }

            public override bool IsRunning()
            {
                return isRunning;
            }

            public override void TurnOff()
            {
                isRunning = false;
            }

            public override void TurnOn()
            {
                isRunning = true;
            }
        }


        [TestMethod]
        public void logicAPITurnOnTurnOffTest()
        {
            FakeDataAPI fakeDataAPI = new FakeDataAPI();
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateAPI(fakeDataAPI);
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
            FakeDataAPI fakeDataAPI = new FakeDataAPI();
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateAPI(fakeDataAPI);
            logicAPI.CreateField(400, 400);
            logicAPI.CreateBalls(10, 10);
            Assert.IsTrue(10 == logicAPI.GetAllBalls().Count);
        }

        [TestMethod]
        public void logicAPILogicBallsMovement()
        {
            FakeDataAPI fakeDataAPI = new FakeDataAPI();
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateAPI(fakeDataAPI);
            logicAPI.CreateField(400, 400);
            logicAPI.CreateBalls(1, 10);

            Vector2 prevPosition = logicAPI.GetAllBalls().Last().Position;

            Thread.Sleep(2000);

            Assert.AreNotEqual(prevPosition.X, logicAPI.GetAllBalls().Last().Position.X);
            Assert.AreNotEqual(prevPosition.Y, logicAPI.GetAllBalls().Last().Position.Y);

        }
    }
}
