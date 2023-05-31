using System.Drawing;
using System.Numerics;
using System.Runtime.Serialization;

namespace Dane
{
    public abstract class IBall
    {
        public static IBall CreateBall(int id, float x, float y)
        {
            return new Ball(id, x, y);
        }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Movement { get; set; }
        public abstract bool IsRunning { get; set; }
        public abstract void TurnOff();

        public abstract event EventHandler<DataEvent> PropertyChanged;


    }
}
