using Logika;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Model
{
    public abstract class AbstractModelAPI 
    {
        public static AbstractModelAPI CreateAPI(AbstractLogicAPI abstractLogic = null) 
        { 
            return new ModelAPI(abstractLogic); 
        }
        public abstract ObservableCollection<IModelBall> GetModelBalls();

        public abstract void TurnOff();
        public abstract void TurnOn(int height, int width, int kulaAmount, int kulaRadius);
        public abstract bool IsRunning();




        internal sealed class ModelAPI : AbstractModelAPI
        {
            private AbstractLogicAPI _logicAPI;
            private ObservableCollection<IModelBall> _ModelBalls = new ObservableCollection<IModelBall>();

            public ModelAPI(AbstractLogicAPI abstractLogicAPI) 
            {
                if (abstractLogicAPI == null)
                {
                    this._logicAPI = AbstractLogicAPI.CreateAPI();  
                }
                else
                {
                    this._logicAPI = abstractLogicAPI;
                }
            }


            private class Unsubscriber : IDisposable
            {
                private IObserver<IModelBall> _observer;

                public Unsubscriber(IObserver<IModelBall> observer)
                {
                    _observer = observer;
                }

                public void Dispose()
                {
                    _observer = null;
                }
            }


            private void CreateField(int height, int width)
            {
                _logicAPI.CreateField(height, width);
            }
            private void CreateBalls(int kulaAmount, int kulaRadius)
            {
                _ModelBalls.Clear();
                _logicAPI.CreateBalls(kulaAmount, kulaRadius);
                foreach (IBall ball in _logicAPI.GetAllBalls())
                {
                    IModelBall modelBall = IModelBall.CreateModelBall(ball.X, ball.Y, ball.R);
                    _ModelBalls.Add(modelBall);
                    ball.PropertyChanged += modelBall.Update!;
                }
            }
            public override ObservableCollection<IModelBall> GetModelBalls()
            {
                return _ModelBalls;
            }
 


            public override bool IsRunning()
            {
                return _logicAPI.IsRunning();
            }
            public override void TurnOff()
            {

                _logicAPI.TurnOff();
            }
            public override void TurnOn(int height, int width, int kulaAmount, int kulaRadius)
            {
                CreateField(height, width);
                CreateBalls(kulaAmount, kulaRadius);
                _logicAPI.TurnOn(); 
            }

        }
    }
}
