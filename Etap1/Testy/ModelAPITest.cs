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
            modelAPI.CreateKule();
            Assert.AreEqual(modelAPI.GetOkragList().Count, 10);
            Assert.AreEqual(modelAPI.GetOkragList().ElementAt(0).R, 10);
            Assert.AreEqual(modelAPI.IsRunning(), false);


            modelAPI.TurnOn();
            Assert.AreEqual(modelAPI.IsRunning(), true);

            double prevX = modelAPI.GetOkragList().First().X;
            double prevY = modelAPI.GetOkragList().First().Y;

            Thread.Sleep(100);

            Assert.AreNotEqual(modelAPI.GetOkragList().First().X, prevX);
            Assert.AreNotEqual(modelAPI.GetOkragList().First().Y, prevY);

            modelAPI.TurnOff();
            Assert.AreEqual(modelAPI.IsRunning(), false);
        }
    }
}
