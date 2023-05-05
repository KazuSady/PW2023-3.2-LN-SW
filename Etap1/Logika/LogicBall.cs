using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Dane;

namespace Logika
{
     internal class LogicBall : ILogicBall, INotifyPropertyChanged
     {
        private Point _position;

        public LogicBall(int x, int y)
        {
            this._position = new Point(x, y);
        }

        public override Point Position
        { get { return _position; } set { _position = value; OnPropertyChanged(); } }

        public override void Update(object obj, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)obj;

            if (args.PropertyName == "Position")
            {
                this.Position = ball.Position;
            }

        }

        public override event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     }
}
