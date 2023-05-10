using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class AbstractDataAPI
    {
        
        public abstract void CreateScene(int height, int width);
        public abstract List<IBall> GetAllBalls();
        public abstract void CreateBall(Point startPosistion);
        public abstract int GetSceneWidth();
        public abstract int GetSceneHeight();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();

        public static AbstractDataAPI CreateApi()
        { 
            return new DataAPI();
        }

        internal sealed class DataAPI : AbstractDataAPI
        {
            private BallList _ballList = new BallList();
            private Scene _scene;


            public override void CreateScene(int height, int width)
            {
                _scene = new Scene(height, width);
            }
            public override List<IBall> GetAllBalls()
            {
                return _ballList.GetAllBalls();
            }
            public override void CreateBall(Point startPosistion)
            {
                _ballList.AddBall(startPosistion);
            }

            public override int GetSceneWidth()
            {
                return _scene.Width;
            }
            public override int GetSceneHeight()
            {
                return _scene.Height;
            }

            public override void TurnOff()
            {
                _ballList.ClearBalls();
                _scene.IsRunning = false;
            }
            public override void TurnOn()
            {
                _scene.IsRunning = true;
            }
            public override bool IsRunning()
            {
                return _scene.IsRunning;  
            }
        }
    }
}
