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
            private BallList _ballList;
            private int sceneHeight;
            private int sceneWidth;
            private bool isRunning;

            public DataAPI()
            {
                _ballList = new BallList();
                isRunning = false;
            }

            public override void CreateScene(int height, int width)
            {
                sceneHeight = height;
                sceneWidth = width;
            }
            public override List<IBall> GetAllBalls()
            {
                return _ballList.GetAllBalls();
            }
            public override void CreateBall(Point startPosistion)
            {
                Ball ball = new Ball(startPosistion.X, startPosistion.Y);
                _ballList.AddBall(ball);
            }

            public override int GetSceneWidth()
            {
                return sceneWidth;
            }
            public override int GetSceneHeight()
            {
                return sceneHeight;
            }

            public override void TurnOff()
            {
                _ballList.ClearBalls();
                isRunning = false;
            }
            public override void TurnOn()
            {
                isRunning = true;
            }
            public override bool IsRunning()
            {
                return isRunning;
            }
        }
    }
}
