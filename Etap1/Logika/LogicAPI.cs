using Dane;

namespace Logika
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateApi(AbstractDaneAPI abstractDaneAPI = null)
        {
            return new LogicAPI(abstractDaneAPI);
        }
        public abstract void CreateObszar(int height, int width, int kulaAmount, int kulaRadius);
        public abstract List<Kula> GetKulaList();
        public abstract void turnOff();
        public abstract void turnOn();
        public abstract bool isRunning();

        internal sealed class LogicAPI : AbstractLogicAPI
        {
            private AbstractDaneAPI dataApi;
            private Obszar obszar;
            public LogicAPI(AbstractDaneAPI abstractDaneAPI = null)
            {
                if (abstractDaneAPI == null)
                {
                    dataApi = AbstractDaneAPI.CreateApi();
                }
                else
                {
                    dataApi = abstractDaneAPI;
                }
            }

            public override void CreateObszar(int height, int width, int kulaAmount, int kulaRadius)
            {
                this.obszar = new Obszar(height, width);
                obszar.CreateKulaList(kulaAmount, kulaRadius);
                foreach(Kula kula in obszar.Kule)
                {
                    Thread thread = new Thread(() => 
                    {
                        while (this.obszar.IsRunning)
                        {
                            Random random = new Random();
                            kula.XMovement = random.Next(-10,10);
                            kula.YMovement = random.Next(-10,10);
                            kula.makeMove();
                            Thread.Sleep(10);
                        }
                    });
                    thread.Start();
                }
            }

            public override List<Kula> GetKulaList()
            {
                return obszar.Kule;
            }
            public override void turnOff()
            {
                this.obszar.IsRunning = false;
            }
            public override void turnOn() 
            {
                this.obszar.IsRunning = true;
            }
            public override bool isRunning()
            {
                return this.obszar.IsRunning;
            }
        }
    }
}