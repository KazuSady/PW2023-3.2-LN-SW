

namespace Dane
{
    public abstract class AbstractBallLogger
    {
        public abstract void addBallToQueue(IBall ball);

        public static AbstractBallLogger CreateBallLoger()
        {
            return new BallLogger();
        }
    }
}
