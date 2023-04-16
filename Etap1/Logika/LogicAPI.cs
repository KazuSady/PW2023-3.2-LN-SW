using Dane;
using System.Runtime.CompilerServices;

using Dane;
using System.Runtime.CompilerServices;

namespace Logika
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateApi(AbstractDaneAPI abstractDaneAPI = null)
        {
            return new LogicAPI(abstractDaneAPI);
        }
        public abstract void CreateObszar(int height, int width, int kulaAmount, int kulaRadius);
        public abstract void CreateKule();
        public abstract List<Logika.Kula> GetKulaList();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();

        internal sealed class LogicAPI : AbstractLogicAPI
        {
            private AbstractDaneAPI dataApi;
            private Logika.Obszar obszar;
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
                obszar = new Obszar(height, width);
                obszar.CreateKulaList(kulaAmount, kulaRadius);
            }
            public override void CreateKule()
            {
                ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

                foreach (Kula kula in obszar.Kule)
                {
                    Task task = new Task(() =>
                    {
                        while (this.IsRunning())
                        {
                            lock (kula)
                            {
                                kula.XMovement = random.Value.Next(-10000, 10000) % 5;
                                kula.YMovement = random.Value.Next(-10000, 10000) % 5;

                                if (0 > (kula.X + kula.XMovement - kula.R) ||
                                    obszar.Width < (kula.X + kula.XMovement + kula.R))
                                {
                                    kula.XMovement = -kula.XMovement;
                                }
                                if (0 > (kula.Y + kula.YMovement - kula.R) ||
                                    obszar.Height < (kula.Y + kula.YMovement + kula.R))
                                {
                                    kula.YMovement = -kula.YMovement;
                                }

                                kula.MakeMove();
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
            public override void TurnOff()
            {
                obszar.IsRunning = false;
                bool isAllTasksCompleted = false;

                while (!isAllTasksCompleted)
                {
                    isAllTasksCompleted = true;
                    foreach (Task task in tasks)
                    {
                        if (!task.IsCompleted)
                        {
                            isAllTasksCompleted = false;
                            break;
                        }
                    }
                }

                foreach (Task task in tasks)
                {
                     task.Dispose();
                }
                tasks.Clear();
                obszar.Kule.Clear();
            }
            public override void TurnOn() 
            {
                obszar.IsRunning = true;
                foreach (Task task in tasks)
                {      
                    try
                    {
                        task.Start();
                    }
                    catch
                    {

                    }
                }
            }
            public override bool IsRunning()
            {
                return obszar.IsRunning;
            }
        }
    }
}