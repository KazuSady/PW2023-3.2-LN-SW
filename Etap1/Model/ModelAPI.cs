using Logika;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelAPI 
    {
        public static AbstractModelAPI CreateAPI(AbstracDataAPI abstractLogicAPI = default) 
        { 
            return new ModelAPI(abstractLogicAPI ?? AbstracDataAPI.CreateAPI()); 
        }
        public abstract ObservableCollection<IModelBall> GetModelBalls();

        public abstract void TurnOff();
        public abstract void TurnOn(int height, int width, int kulaAmount, int kulaRadius);
        public abstract bool IsRunning();


        internal sealed class ModelAPI : AbstractModelAPI
        {
            private AbstracDataAPI _logicAPI;
            private ObservableCollection<IModelBall> _ModelBalls = new ObservableCollection<IModelBall>();
            public ModelAPI(AbstracDataAPI abstractLogicAPI) 
            {
                _logicAPI = abstractLogicAPI; 
            }



            private void CreateField(int height, int width)
            {
                _logicAPI.CreateField(height, width);
            }
            private void CreateBalls(int ballAmount, int ballRadius)
            {         
                _ModelBalls.Clear();
                _logicAPI.CreateBalls(ballAmount, ballRadius);
                foreach (ILogicBall logicBall in _logicAPI.GetAllBalls())
                {
                    IModelBall modelBall = IModelBall.CreateModelBall(logicBall.Position.X, logicBall.Position.Y, ballRadius);
                    _ModelBalls.Add(modelBall);
                    logicBall.PropertyChanged += modelBall.Update!;
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
                _logicAPI.ClearBalls();
                _ModelBalls.Clear();
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
