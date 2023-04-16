﻿using Logika;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Model
{
    public abstract class AbstractModelAPI
    {
        public static AbstractModelAPI CreateAPI(AbstractLogicAPI abstractLogic = null) 
        { 
            return new ModelAPI(abstractLogic); 
        }
        public abstract void CreateObszar(int height, int width, int kulaAmount, int kulaRadius);
        public abstract void CreateKule();
        public abstract ObservableCollection<Okrag> GetOkragList();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();

        internal sealed class ModelAPI : AbstractModelAPI
        {
            private AbstractLogicAPI logicAPI;
            private ObservableCollection<Okrag> okregi = new ObservableCollection<Okrag>();

            public ModelAPI(AbstractLogicAPI abstractLogicAPI) 
            {
                if (abstractLogicAPI == null)
                {
                    this.logicAPI = AbstractLogicAPI.CreateApi();
                }
                else
                {
                    this.logicAPI = abstractLogicAPI;
                }
            }

            public ObservableCollection<Okrag> Okregi
            { get { return this.okregi; } set { this.okregi = value; } }

            public override void CreateObszar(int height, int width, int kulaAmount, int kulaRadius)
            {
                logicAPI.CreateObszar(height, width, kulaAmount, kulaRadius);
            }
            public override void CreateKule()
            {
                logicAPI.CreateKule();
            }
            public override ObservableCollection<Okrag> GetOkragList()
            {
                okregi.Clear();
                foreach (Kula kula in logicAPI.GetKulaList())
                {
                    okregi.Add(new Okrag(kula));
                }
                return okregi;
            }

            public override bool IsRunning()
            {
                return logicAPI.IsRunning();
            }
            public override void TurnOff()
            {
                logicAPI.TurnOff();
            }
            public override void TurnOn()
            {
                logicAPI.TurnOn(); 
            }
        }
    }
}
