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
            private List<Task> tasks = new List<Task>();
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
                    Task task = Task.Run(() =>
                    {
                        while (this.obszar.IsRunning)
                        {
                            Random random = new Random();
                            lock (kula)
                            {
                                kula.XMovement = random.Next(-10, 10);
                                kula.YMovement = random.Next(-10, 10);
                                kula.makeMove();
                                Thread.Sleep(10);
                            }
                        }
                    });
                    tasks.Add(task);
                }
            }

            public override List<Kula> GetKulaList()
            {
                return obszar.Kule;
            }
            public override void turnOff()
            {
                this.obszar.IsRunning = false;
                disposeOfThreads();
            }
            public override void turnOn() 
            {
                this.obszar.IsRunning = true;
            }
            public override bool isRunning()
            {
                return this.obszar.IsRunning;
            }
            private void disposeOfThreads()
            {
                foreach(Task task in tasks)
                {
                    task.Dispose();
                }
            }
        }
    }
}