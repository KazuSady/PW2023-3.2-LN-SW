using Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    [TestClass]
    internal class LogicAPITest
    {
        [TestMethod]
        public void logicAPITest()
        {
            AbstractLogicAPI logicAPI = AbstractLogicAPI.CreateApi();
            logicAPI.CreateObszar(400, 400, 10, 10);
            Assert.AreEqual(logicAPI.GetKulaList().Count, 10);
            Assert.AreEqual(logicAPI.GetKulaList().ElementAt(0).R, 10);
            Assert.AreEqual(false, logicAPI.isRunning());

            logicAPI.turnOn();
            Assert.AreEqual(true, logicAPI.isRunning());

            logicAPI.turnOff();
            Assert.AreEqual(false, logicAPI.isRunning());
        }
    }
}
