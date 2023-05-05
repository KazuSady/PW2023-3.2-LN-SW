using Model;

namespace Testy
{
    [TestClass]
    public class ModelAPITest
    {
        [TestMethod]
        public void modelAPIGettersTest()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();
            modelAPI.TurnOn(400, 400, 10, 10);
            Assert.AreEqual(true, modelAPI.IsRunning());

            modelAPI.TurnOff();
            Assert.AreEqual(false, modelAPI.IsRunning());
        }

        [TestMethod]
        public void modelAPICreateBallTest()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();
            modelAPI.TurnOn(400, 400, 10, 10);
            Assert.IsTrue(10 == modelAPI.GetModelBalls().Count);
        }

        [TestMethod]
        public void modelAPIBallsMovingTest()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();
            modelAPI.TurnOn(400, 400, 10, 10);
            int prevX = modelAPI.GetModelBalls().First().Position.X;
            int prevY = modelAPI.GetModelBalls().First().Position.Y;

            Thread.Sleep(100);

            Assert.IsFalse(prevX == modelAPI.GetModelBalls().First().Position.X);
            Assert.IsFalse(prevY == modelAPI.GetModelBalls().First().Position.Y);

        }

    }
}
