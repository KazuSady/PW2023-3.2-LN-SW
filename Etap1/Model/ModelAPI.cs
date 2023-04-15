using Logika;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelAPI
    {
        public static AbstractModelAPI CreateAPI(AbstractLogicAPI abstractLogic = null) 
        { 
            return new ModelAPI(abstractLogic); 
        }
        public abstract void CreateObszar(int height, int width, int kulaAmount, int kulaRadius);
        public abstract ObservableCollection<Okrag> GetOkragList();
        public abstract void turnOff();
        public abstract void turnOn();
        public abstract bool isRunning();

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
            public override ObservableCollection<Okrag> GetOkragList()
            {
                this.okregi.Clear();

                List<Kula> kule = logicAPI.GetKulaList();
                foreach (Kula kula in kule)
                {
                    okregi.Add(new Okrag(kula));
                }
                return okregi;
            }

            public override bool isRunning()
            {
                return logicAPI.isRunning();
            }
            public override void turnOff()
            {
                logicAPI.turnOff();
            }
            public override void turnOn()
            {
                logicAPI.turnOn(); 
            }
        }
    }
}
