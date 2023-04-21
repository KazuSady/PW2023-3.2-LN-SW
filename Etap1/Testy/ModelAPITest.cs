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
            modelAPI.CreateField(400, 400);
            modelAPI.CreateBalls(10, 10);

            Assert.AreEqual(false, modelAPI.IsRunning());

            modelAPI.TurnOn();
            Assert.AreEqual(true, modelAPI.IsRunning());

            modelAPI.TurnOff();
            Assert.AreEqual(false, modelAPI.IsRunning());
        }

        [TestMethod]
        public void logicAPIModelBallsAreMoving()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateAPI();
            modelAPI.CreateField(400, 400);
            modelAPI.CreateBalls(10, 10);

            double prevX = modelAPI.GetModelBallX(0);
            double prevY = modelAPI.GetModelBallY(0);

            Thread.Sleep(100);

            Assert.AreNotEqual(prevX, modelAPI.GetModelBallX(0));
            Assert.AreNotEqual(prevY, modelAPI.GetModelBallY(0));
        }
        
    }
}
