using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Dane;

namespace Logika
{
    internal class LogicBall : ILogicBall
    {
        private Point _position;

        public LogicBall(int x, int y)
        {
            this._position = new Point(x, y);
        }

        public override Point Position
        { get { return _position; } set { _position = value; } }

        public override void Update(Object o, DataEvent args)
        {
            IBall ball = (IBall)o;
            this.Position = ball.Position;
            LogicEvent e = new LogicEvent(this);
            PropertyChanged?.Invoke(this, e);
        }

        public override event EventHandler<LogicEvent>? PropertyChanged;
        /*
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
