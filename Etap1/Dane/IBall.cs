using System.Drawing;

namespace Dane
{
    public abstract class IBall
    {
        public static IBall CreateBall(int x, int y)
        {
            return new Ball(x, y);
        }

        public abstract Point Position { get; set; }
        public abstract int XMovement { get; set; }
        public abstract int YMovement { get; set; }
        public abstract bool IsRunning { get; set; }
        public abstract void TurnOff();

        public abstract event EventHandler<DataEvent> PropertyChanged;

    }
}
