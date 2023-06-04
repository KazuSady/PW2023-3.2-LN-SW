

namespace Dane
{
    public abstract class AbstractBallLogger
    {
        public abstract void addBallToQueue(IBall ball);

        public static AbstractBallLogger CreateBallLoger(int sceneHeigth, int sceneWidth)
        {
            return new BallLogger(sceneHeigth, sceneWidth);
        }
    }
}
