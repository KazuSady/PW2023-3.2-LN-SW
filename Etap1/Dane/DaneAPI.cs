using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class AbstractDaneAPI
    {
        public abstract void CreateObszar(int height, int width, int kulaAmount ,int kulaRadius);
        public abstract Obszar Obszar { get; }
        public abstract List<Kula> GetKulaList();
        public abstract void turnOff();
        public static AbstractDaneAPI CreateApi()
        {
            return new DaneAPI();
        }

        internal sealed class DaneAPI : AbstractDaneAPI
        {
            private bool isRunning;

            private Obszar obszar;
            public bool Running { get { return isRunning; } set { isRunning = value; } }
            public override Obszar Obszar { get { return obszar; } }

            public override List<Kula> GetKulaList()
            {
                return this.Obszar.Kule;
            }

            public override void turnOff()
            {
                this.isRunning = false;
            }

            public override void CreateObszar(int height, int width, int kulaAmount, int kulaRadius)
            {
                this.obszar = new Obszar(height, width, kulaAmount, kulaRadius);
                
                List<Kula> kule = GetKulaList();

                foreach (Kula kula in kule)
                {
                    Thread thread = new Thread(() =>
                    {
                        while (true)
                        {
                            kula.makeMove();
                        }
                        Thread.Sleep(10); 
                    });
                    thread.Start();
                }

            }
    
        }
    }
}
